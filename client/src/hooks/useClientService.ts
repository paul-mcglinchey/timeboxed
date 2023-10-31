import { IClient, IClientsResponse, IGroupClientTagResponse, ISession, IUpdateSessionRequest } from "../models"
import { ClientContext } from "../contexts/ClientContext"
import { GroupContext } from "../contexts/GroupContext"
import { endpoints } from '../config'
import { useRequestBuilderService, useAsyncHandler, useResolutionService } from '.'
import { Dispatch, SetStateAction, useContext, useMemo } from "react"
import { IClientService } from "./interfaces"
import { IApiError } from "../models/error.model"
import { IListResponse } from "../models/list-response.model"

const useClientService = (setIsLoading: Dispatch<SetStateAction<boolean>>, setError: Dispatch<SetStateAction<IApiError | undefined>>): IClientService => {

  const clientContext = useContext(ClientContext)
  const { buildQueryString } = clientContext
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
  }), [asyncReturnHandler, buildRequest, groupId, setError])

  const getGroupClientTags = asyncReturnHandler<IGroupClientTagResponse[]>(async () => {
    if (!groupId) throw new Error()

    const res = await fetch(endpoints.clienttags(groupId), buildRequest('GET'))
    const json: IGroupClientTagResponse[] = await res.json()

    return json
  })

  const addClient = asyncHandler(async (values: IClient) => {
    if (!groupId) throw new Error()

    const res = await fetch(`${endpoints.clients(groupId)}?${buildQueryString()}`, buildRequest('POST', undefined, values))
    const json: IClientsResponse = await res.json()

    handleResolution(res, json, 'create', 'client')
  })

  const updateClient = asyncHandler(async (clientId: string, values: IClient) => {
    if (!groupId) throw new Error()

    const res = await fetch(endpoints.client(clientId, groupId), buildRequest('PUT', undefined, values))

    handleResolution(res, null, 'update', 'client')
  })

  const deleteClient = asyncHandler(async (clientId: string) => {
    if (!groupId) throw new Error()

    const res = await fetch(endpoints.client(clientId, groupId), buildRequest("DELETE"))
    const json: IClientsResponse = await res.json()

    handleResolution(res, json, 'delete', 'client')
  })

  const getSessions = asyncReturnHandler<IListResponse<ISession>>(async (clientId: string, tagId?: string) => {
    if (!groupId) throw new Error()

    const res = await fetch(endpoints.sessions(clientId, groupId, tagId), buildRequest('GET'))
    const json: ISession[] = await res.json()

    return json
  })

  const getSessionById = asyncReturnHandler<ISession>(async (clientId: string, sessionId: string) => {
    if (!groupId) throw new Error()
  
    const res = await fetch(endpoints.session(clientId, groupId, sessionId), buildRequest('GET'))
    const json: ISession = await res.json()

    return json
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
    const json = await res.json()

    handleResolution(res, json, 'delete', 'session')
  })

  return { ...clientContext, getClient, getGroupClientTags, addClient, deleteClient, updateClient, getSessions, getSessionById, addSession, updateSession, deleteSession }
}

export default useClientService