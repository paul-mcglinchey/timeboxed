import { IGroupContext } from "../../contexts/interfaces"
import { IGroupRequest } from "../../models/group.model"

export interface IGroupService extends IGroupContext {
  addGroup: (values: IGroupRequest) => Promise<void>
  updateGroup: (values: IGroupRequest, groupId: string | undefined) => Promise<void>
  adminUpdateGroup: (values: IGroupRequest, groupId: string) => Promise<void>
  deleteGroup: (groupId: string | undefined) => Promise<void>
}