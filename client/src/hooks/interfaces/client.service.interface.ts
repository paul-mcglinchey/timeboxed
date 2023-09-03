import { IClientContext } from "../../contexts/interfaces";
import { IAddClientRequest, IAddSessionRequest, IClient, IGroupClientTagResponse, ISession, IUpdateClientRequest, IUpdateSessionRequest } from "../../models";
import { IListResponse } from "../../models/list-response.model";

export interface IClientService extends IClientContext {
  getClient: (clientId: string) => Promise<IClient | undefined>
  getGroupClientTags: () => Promise<IGroupClientTagResponse[]>
  addClient: (values: IAddClientRequest) => Promise<void>
  updateClient: (clientId: string, values: IUpdateClientRequest) => Promise<void>
  deleteClient: (clientId: string) => Promise<void>
  getSessions: (clientId: string, tagId?: string) => Promise<IListResponse<ISession>>
  getSessionById: (clientId: string, sessionId: string) => Promise<ISession>
  addSession: (clientId: string, values: IAddSessionRequest) => Promise<void>
  updateSession: (clientId: string, sessionId: string, values: IUpdateSessionRequest) => Promise<void>
  deleteSession: (clientId: string, sessionId: string) => Promise<void>
}