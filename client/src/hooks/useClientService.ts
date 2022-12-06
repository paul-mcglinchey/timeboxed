import { IClient, IClientListResponse, IClientsResponse, ISession, IUpdateSessionRequest } from "../models"
import { ClientContext, GroupContext } from "../contexts"
import { endpoints } from '../config'
import { useRequestBuilderService, useAsyncHandler, useResolutionService } from '.'
import { Dispatch, SetStateAction, useContext, useMemo } from "react"
import { IClientService } from "./interfaces"
import { IApiError } from "../models/error.model"

const useClientService = (setIsLoading: Dispatch<SetStateAction<boolean>>, setError: Dispatch<SetStateAction<IApiError | undefined>>): IClientService => {

  const clientContext = useContext(ClientContext)
  const { setClients, setCount, buildQueryString } = clientContext
  const { currentGroup } = useContext(GroupContext)

  const { buildRequest } = useRequestBuilderService()
  const { asyncHandler, asyncReturnHandler } = useAsyncHandler(setIsLoading)
  const { handleResolution } = useResolutionService()

  const groupId: string | undefined = currentGroup?.id

  const getClient = useMemo(() => asyncReturnHandler<IClient | undefined>(async (clientId: string) => {
    if (!groupId) throw new Error()

    const res = await fetch(endpoints.client(clientId, groupId), buildRequest('GET'))

    if (!res.ok && res.status < 500) return setError(await res.json())

    const json: IClient = await res.json()

    return json
  }), [asyncReturnHandler, buildRequest, groupId])

  const addClient = asyncHandler(async (values: IClient) => {
    if (!groupId) throw new Error()

    const res = await fetch(`${endpoints.clients(groupId)}?${buildQueryString()}`, buildRequest('POST', undefined, values))
    const json: IClientsResponse = await res.json()

    handleResolution(res, json, 'create', 'client', [() => updateClientsInContext(json)])
  })

  const updateClient = asyncHandler(async (clientId: string, values: IClient) => {
    if (!groupId) throw new Error()

    const res = await fetch(endpoints.client(clientId, groupId), buildRequest('PUT', undefined, values))
    const json = await res.json()

    handleResolution(res, json, 'update', 'client', [() => updateClientInContext(clientId, json)])
  })

  const deleteClient = asyncHandler(async (clientId: string) => {
    if (!groupId) throw new Error()

    const res = await fetch(endpoints.client(clientId, groupId), buildRequest("DELETE"))
    const json: IClientsResponse = await res.json()

    handleResolution(res, json, 'delete', 'client', [() => updateClientsInContext(json)])
  })

  const addSession = asyncHandler(async (clientId: string, values: ISession) => {
    if (!groupId) throw new Error()

    const res = await fetch((endpoints.sessions(clientId, groupId)), buildRequest('POST', undefined, values))
    const json: ISession = await res.json()

    handleResolution(res, json, 'create', 'session')
  })

  const updateSession = asyncHandler(async (clientId: string, sessionId: string, values: IUpdateSessionRequest) => {
    if (!groupId) throw new Error()

    const res = await fetch((endpoints.session(clientId, groupId, sessionId)), buildRequest('PUT', undefined, values))
    const json: ISession = await res.json()

    handleResolution(res, json, 'update', 'session')
  })

  const deleteSession = asyncHandler(async (clientId: string, sessionId: string) => {
    if (!groupId) throw new Error()

    const res = await fetch((endpoints.session(clientId, groupId, sessionId)), buildRequest('DELETE'))
    const json: string = await res.json()

    handleResolution(res, json, 'delete', 'session')
  })

  const updateClientsInContext = (values: IClientsResponse) => {
    setClients(values.items)
    setCount(values.count)
  }

  const updateClientInContext = (clientId: string, values: IClientListResponse) => {
    setClients(clients => {
      return clients.map(c => {
        return c.id === clientId ? { ...c, ...values } : c
      })
    })
  }

  return { ...clientContext, getClient, addClient, deleteClient, updateClient, addSession, updateSession, deleteSession }
}

export default useClientService