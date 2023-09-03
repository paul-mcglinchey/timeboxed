import { useEffect, useState } from "react";
import { SessionTableRow } from ".";
import { useClientService } from "../../hooks";
import { IClient, ISession } from "../../models";
import { IApiError } from "../../models/error.model";
import { SearchBar, SpinnerLoader, Table } from "../Common";

interface ISessionListProps {
  client: IClient
}

const headers = [
  { name: "Title", value: "title", interactive: false },
  { name: "Tags", value: "tags", interactive: false },
  { name: "Options", value: "", interactive: false },
]

const SessionList = ({ client }: ISessionListProps) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError | undefined>()

  const [count, setCount] = useState<number>(0)
  const [sessions, setSessions] = useState<ISession[]>([])

  const [tagIdFilter, setTagIdFilter] = useState<string | undefined>()

  const { getSessions } = useClientService(setIsLoading, setError)
  
  const fetchSessions = async () => {
    let response = await getSessions(client.id, tagIdFilter)
    setSessions(response.items)
    setCount(response.count)
  }

  useEffect(() => {
    fetchSessions()
  }, [tagIdFilter])

  return (
    <>
      {count > 0 ? (
        <div className="flex flex-col flex-grow space-y-4">
          <SearchBar setFilter={value => setTagIdFilter(value)} />
          <Table isLoading={isLoading}>
            <Table.Header headers={headers} />
            <Table.Body>
              {sessions.map((s: ISession, index: number) => (
                <SessionTableRow clientId={client.id} session={s} key={index} setIsLoading={setIsLoading} setError={setError} fetchSessions={fetchSessions} />
              ))}
            </Table.Body>
          </Table>
        </div>
      ) : isLoading ? <SpinnerLoader /> : error ? Error : null}
    </>
  )
}

export default SessionList;