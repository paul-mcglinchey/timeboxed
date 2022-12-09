import { IGroupContext } from "../../contexts/interfaces"
import { IGroupRequest } from "../../models/group.model"

export interface IGroupService extends IGroupContext {
  addGroup: (values: IGroupRequest) => void
  updateGroup: (values: IGroupRequest, groupId: string | undefined) => void
  deleteGroup: (groupId: string | undefined) => void
}