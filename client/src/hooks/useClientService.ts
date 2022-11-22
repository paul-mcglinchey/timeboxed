import { IClient, ISession, IUpdateSessionRequest } from "../models"
import { ClientContext } from "../contexts"
import { endpoints } from '../config'
import { useRequestBuilder, useAsyncHandler, useResolutionService, useGroupService } from '.'
import { useContext } from "react"
import { IClientService } from "./interfaces"

const useClientService = (): IClientService => {

  const clientContext = useContext(ClientContext)
  const { clients, setClients, setCount, setIsLoading } = clientContext

  const { requestBuilder } = useRequestBuilder()
  const { asyncHandler } = useAsyncHandler(setIsLoading)
  const { handleResolution } = useResolutionService()
  const { currentGroup } = useGroupService()

  const groupId: string | undefined = currentGroup?.id

  const getClient = (clientId: string): IClient | undefined => {
    return clients.find(c => c.id === clientId)
  }

  const addClient = asyncHandler(async (values: IClient) => {
    if (!groupId) throw new Error()

    const res = await fetch(endpoints.clients(groupId), requestBuilder('POST', undefined, values))
    const json: IClient = await res.json()

    handleResolution(res, json, 'create', 'client', [() => addClientInContext(json)])
  })

  const updateClient = asyncHandler(async (clientId: string, values: IClient) => {
    if (!groupId) throw new Error()

    const res = await fetch(endpoints.client(clientId, groupId), requestBuilder('PUT', undefined, values))
    const json = await res.json()

    handleResolution(res, json, 'update', 'client', [() => updateClientInContext(clientId, json)])
  })

  const deleteClient = asyncHandler(async (clientId: string) => {
    if (!groupId) throw new Error()

    const res = await fetch(endpoints.client(clientId, groupId), requestBuilder("DELETE"))
    const json = await res.json()

    handleResolution(res, json, 'delete', 'client', [() => deleteClientInContext(clientId)])
  })

  const getSession = (clientId: string, sessionId: string): ISession | undefined => clients.find(c => c.id === clientId)?.sessions.find(s => s.id === sessionId)

  const addSession = asyncHandler(async (clientId: string, values: ISession) => {
    if (!groupId) throw new Error()

    const res = await fetch((endpoints.sessions(clientId, groupId)), requestBuilder('POST', undefined, values))
    const json: ISession = await res.json()

    handleResolution(res, json, 'create', 'session', [() => addSessionInContext(clientId, json)])
  })

  const updateSession = asyncHandler(async (clientId: string, sessionId: string, values: IUpdateSessionRequest) => {
    if (!groupId) throw new Error()

    const res = await fetch((endpoints.session(clientId, groupId, sessionId)), requestBuilder('PUT', undefined, values))
    const json: ISession = await res.json()

    handleResolution(res, json, 'update', 'session', [() => updateSessionInContext(clientId, sessionId, json)])
  })

  const deleteSession = asyncHandler(async (clientId: string, sessionId: string) => {
    if (!groupId) throw new Error()

    const res = await fetch((endpoints.session(clientId, groupId, sessionId)), requestBuilder('DELETE'))
    const json: string = await res.json()

    handleResolution(res, json, 'delete', 'session', [() => deleteSessionInContext(clientId, json)])
  })

  const addClientInContext = (client: IClient) => {
    setClients(clients => [...clients, client])
    setCount(c => c + 1)
  }

  const deleteClientInContext = (clientId: string) => {
    setClients(clients => clients.filter(c => c.id !== clientId))
    setCount(c => c - 1)
  }

  const updateClientInContext = (clientId: string, values: IClient) => {
    setClients(clients => {
      return clients.map(c => {
        return c.id === clientId ? { ...c, ...values } : c
      })
    })
  }

  const addSessionInContext = (clientId: string, values: ISession) => {
    setClients(clients => clients.map(c => c.id === clientId ? { ...c, sessions: [...c.sessions, values]} : c))
  }
  
  const updateSessionInContext = (clientId: string, sessionId: string, values: ISession) => {
    setClients(clients => clients.map(c => c.id === clientId ? { ...c, sessions: c.sessions.map(s => s.id === sessionId ? { ...s, ...values} : s) } : c))
  }

  const deleteSessionInContext = (clientId: string, sessionId: string) => {
    setClients(clients => clients.map(c => c.id === clientId ? { ...c, sessions: c.sessions.filter(s => s.id !== sessionId) } : c))
  }

  return { ...clientContext, getClient, addClient, deleteClient, updateClient, getSession, addSession, updateSession, deleteSession }
}

export default useClientService