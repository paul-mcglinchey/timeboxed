import { IFilter, IGroupUser, IGroupUserInviteRequest, IGroupUserRequest } from "../../models"
import { IListResponse } from "../../models/list-response.model"

export interface IGroupUserService {
  getGroupUsers: (groupId: string, filters?: IFilter[]) => Promise<IListResponse<IGroupUser>>
  updateGroupUser: (groupId: string | undefined, userId: string | undefined, values: IGroupUserRequest) => Promise<void>
  inviteGroupUser: (groupId: string | undefined, values: IGroupUserInviteRequest) => Promise<void>
  uninviteGroupUser: (groupId: string | undefined, userId: string | undefined) => Promise<void>
  joinGroup: (groupId: string | undefined) => Promise<void>
}