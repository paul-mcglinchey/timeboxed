import { CheckIcon, InformationCircleIcon, PlusIcon, XMarkIcon } from "@heroicons/react/24/solid"
import { IAdminUser, IContextualFormProps, IFilter, IFilterableField, IGroup, IUser } from "../../models"
import { useCallback, useEffect, useState } from "react"
import { useGroupService, useRequestBuilderService } from "../../hooks"
import { endpoints } from "../../config"
import { IListResponse } from "../../models/list-response.model"
import { InlineButton, SearchBar, SpinnerLoader, Table, TableRow, TableRowItem } from ".."
import { IApiError } from "../../models/error.model"

const filters: IFilterableField[] = [
  { label: 'Username', name: 'username' },
  { label: 'Email', name: 'email' }
]

interface IUpdateGroupUsers extends IContextualFormProps {
  groupId: string
}

export const UpdateGroupUsers = ({ groupId, ContextualSubmissionButton }: IUpdateGroupUsers) => {

  const [loading, setLoading] = useState<boolean>(false)
  const [addUserLoading, setAddUserLoading] = useState<boolean>(false)
  const [addUserError, setAddUserError] = useState<IApiError | undefined>()
  const [group, setGroup] = useState<IGroup | undefined>()
  const [users, setUsers] = useState<IAdminUser[]>([])
  const [count, setCount] = useState<number>(0)
  const [filter, setFilter] = useState<IFilter>({ value: null, label: null, name: null })

  const { buildRequest } = useRequestBuilderService()
  const { adminUpdateGroup } = useGroupService(setAddUserLoading, setAddUserError)

  const fetchGroup = useCallback(async (): Promise<void> => {
    setLoading(true)
    const res = await fetch(endpoints.admin.group(groupId), buildRequest('GET'))
    const json: IGroup = await res.json()

    setGroup(json)
    setLoading(false)
  }, [])

  const fetchUsers = useCallback(async (): Promise<void> => {
    setLoading(true)
    const res = await fetch(`${endpoints.admin.users}?${filter.name}=${filter.value}`, buildRequest('GET'))
    const json: IListResponse<IAdminUser> = await res.json()

    setUsers(json.items)
    setCount(json.count)
    setLoading(false)
  }, [filter])

  const addUser = async (userId: string) => {
    if (!group) return
    await adminUpdateGroup({ users: [...group.users.map(u => u.userId), userId], applications: group.applications }, group.id)
    fetchGroup()
    fetchUsers()
  }

  useEffect(() => {
    fetchGroup()

    if (filter.value) {
      fetchUsers()
    } else {
      setUsers([])
      setCount(0)
    }
  }, [fetchUsers, filter])

  return (
    <div>
      <div className="flex flex-col gap-6">
        <div>
          <h3 className="font-bold text-xl tracking-wide dark:text-blue-200 text-blue-800 mb-2">Current users</h3>
          <Table isLoading={loading} compact>
            <Table.Body>
              {group && group.users.map((u, i) => (
                <TableRow key={i}>
                  <TableRowItem>
                    <div className="flex flex-col gap-1">
                      <span>{u.username}</span>
                      <span className="text-xs">{u.email}</span>
                    </div>
                  </TableRowItem>
                  <TableRowItem hugRight>
                    <div className="flex justify-between items-center gap-2">
                      <span>
                        Has joined?
                      </span>
                      {u.hasJoined ? <CheckIcon className="w-6 h-6 text-green-500" /> : <XMarkIcon className="w-6 h-6 text-red-500" />}
                    </div>
                  </TableRowItem>
                </TableRow>
              ))}
            </Table.Body>
          </Table>
        </div>
        <div>
          <h3 className="font-bold text-xl tracking-wide dark:text-blue-200 text-blue-800 mb-2">Add users</h3>
          <div className="flex flex-col gap-4">
            <SearchBar setFilter={setFilter} initialFilterField={filters[0]} filterableFields={filters} id="user-search-bar" backgroundColorClasses="dark:bg-gray-900 bg-gray-200" />
            {(count > 0) ? (
              <Table isLoading={loading} compact>
                <Table.Body>
                  {users.filter(u => !group?.users.map(gu => gu.userId).includes(u.id)).map((u, i) => (
                    <TableRow key={i}>
                      <TableRowItem>
                        <div className="flex flex-col gap-1">
                          <span>{u.username}</span>
                          <span className="text-xs">{u.email}</span>
                        </div>
                      </TableRowItem>
                      <TableRowItem hugRight>
                        <InlineButton action={() => addUser(u.id)} color="text-green-500"><PlusIcon className="w-6 h-6" /></InlineButton>
                      </TableRowItem>
                    </TableRow>
                  ))}
                </Table.Body>
              </Table>
            ) : (
              loading ? (
                <div className="my-4">
                  <SpinnerLoader className="w-6 h-6" />
                </div>
              ) : (
                <div className="flex items-center justify-between px-4 py-2 my-4 bg-blue-200/40 font-bold rounded-md flex-row-reverse"><InformationCircleIcon className="w-5 h-5" />Use the search bar to find users</div>
              )
            )}
          </div>
        </div>
      </div>
      {ContextualSubmissionButton('Save changes')}
    </div>
  )
}