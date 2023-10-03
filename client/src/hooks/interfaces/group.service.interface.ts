import { IGroupContext } from "../../contexts/interfaces"
import { IGroup, IGroupRequest } from "../../models/group.model"

export interface IGroupService extends IGroupContext {
  getGroupFromContext: (groupId: string) => IGroup | undefined
  getGroup: (groupId: string) => Promise<IGroup>
  addGroup: (values: IGroupRequest) => Promise<void>
  updateGroup: (values: IGroupRequest, groupId: string | undefined) => Promise<void>
  adminUpdateGroup: (values: IAdminGroupRequest, groupId: string) => Promise<void>
  deleteGroup: (groupId: string | undefined) => Promise<void>
}