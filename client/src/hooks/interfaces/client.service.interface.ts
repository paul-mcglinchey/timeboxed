import { IClientContext } from "../../contexts/interfaces";
import { IAddClientRequest, IAddSessionRequest, IClient, ISession, IUpdateClientRequest, IUpdateSessionRequest } from "../../models";

export interface IClientService extends IClientContext {
  getClient: (clientId: string) => IClient | undefined
  addClient: (values: IAddClientRequest) => void
  updateClient: (clientId: string, values: IUpdateClientRequest) => void
  deleteClient: (clientId: string) => void
  getSession: (clientId: string, sessionId: string) => ISession | undefined
  addSession: (clientId: string, values: IAddSessionRequest) => void
  updateSession: (clientId: string, sessionId: string, values: IUpdateSessionRequest) => void
  deleteSession: (clientId: string, sessionId: string) => void
}