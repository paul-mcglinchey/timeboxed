import { IClientContext } from "../../contexts/interfaces";
import { IAddClientRequest, IAddSessionRequest, IClient, IUpdateClientRequest, IUpdateSessionRequest } from "../../models";

export interface IClientService extends IClientContext {
  getClient: (clientId: string) => Promise<IClient | undefined>
  addClient: (values: IAddClientRequest) => void
  updateClient: (clientId: string, values: IUpdateClientRequest) => void
  deleteClient: (clientId: string) => void
  addSession: (clientId: string, values: IAddSessionRequest) => void
  updateSession: (clientId: string, sessionId: string, values: IUpdateSessionRequest) => void
  deleteSession: (clientId: string, sessionId: string) => void
}