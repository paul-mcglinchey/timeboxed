  import { useEffect, useState } from "react";
import {
  Navigate,
  Route,
  Routes,
  useParams
} from "react-router";
import { ClientHeader, ClientOverview } from ".";
import { useClientService } from "../../hooks";
import { IClient } from "../../models";

const ClientPage = () => {

  const [client, setClient] = useState<IClient | undefined>()
 
  const { clientId } = useParams()
  const { getClient } = useClientService()

  useEffect(() => {
    clientId && setClient(getClient(clientId))
  }, [clientId, getClient])

  return (
    <>
      {client ? (
        <div className="flex flex-col">
          <ClientHeader client={client} />
          <Routes>
            <Route path="view" element={<ClientOverview client={client} />} />
            <Route path="*/*" element={<Navigate to="view" />} />
          </Routes>
        </div>
      ) : (
        <>
          Loading dat client
        </>
      )}
    </>
  )
}

export default ClientPage;