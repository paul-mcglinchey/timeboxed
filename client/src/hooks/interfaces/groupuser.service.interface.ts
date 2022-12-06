import { IGroup, IGroupUser, IGroupUserInviteRequest, IGroupUserRequest, ILoadable } from "../../models"

export interface IGroupUserService extends ILoadable {
  getGroupUser: (userId: string | undefined, group?: IGroup | undefined) => IGroupUser | undefined
  updateGroupUser: (groupId: string | undefined, userId: string | undefined, values: IGroupUserRequest) => void
  inviteGroupUser: (groupId: string | undefined, values: IGroupUserInviteRequest) => void
  uninviteGroupUser: (groupId: string | undefined, userId: string | undefined) => void
  joinGroup: (groupId: string | undefined) => void
}