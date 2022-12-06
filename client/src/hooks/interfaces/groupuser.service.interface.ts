import { IGroupUserInviteRequest, IGroupUserRequest } from "../../models"

export interface IGroupUserService {
  updateGroupUser: (groupId: string | undefined, userId: string | undefined, values: IGroupUserRequest) => void
  inviteGroupUser: (groupId: string | undefined, values: IGroupUserInviteRequest) => void
  uninviteGroupUser: (groupId: string | undefined, userId: string | undefined) => void
  joinGroup: (groupId: string | undefined) => void
}