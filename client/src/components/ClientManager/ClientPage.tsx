import { lazy, useEffect, useState } from "react";
import {
  Navigate,
  Route,
  Routes,
  useParams
} from "react-router";
import { useClientService } from "../../hooks";
import { IClient } from "../../models";
import { IApiError } from "../../models/error.model";
import { SpinnerLoader } from "../Common";
import ClientHeader from './ClientHeader'
const ClientOverview = lazy(() => import('./ClientOverview'))
const SessionList = lazy(() => import('./SessionList'))

const ClientPage = () => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()

  const [client, setClient] = useState<IClient | undefined>()
 
  const { clientId } = useParams()
  const { getClient } = useClientService(setIsLoading, setError)

  useEffect(() => {
    const _fetch = async () => {
      if (clientId) {
        setClient(await getClient(clientId))
      }
    }
    
    _fetch()
  }, [clientId, getClient])

  return (
    <>
      {client ? (
        <div className="flex flex-col">
          <ClientHeader client={client} />
          <Routes>
            <Route path="view" element={<ClientOverview client={client} />} />
            <Route path="sessions" element={<SessionList client={client} />} />
            <Route path="*/*" element={<Navigate to="view" />} />
          </Routes>
        </div>
      ) : (
        isLoading ? <SpinnerLoader /> : error ? <span className="text-red-500">{error.message}</span> : null
      )}
    </>
  )
}

export default ClientPage;