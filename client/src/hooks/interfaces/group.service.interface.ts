import { IGroupContext } from "../../contexts/interfaces"
import { IGroup, IGroupRequest } from "../../models"

export interface IGroupService extends IGroupContext {
  getGroup: (groupId: string | undefined) => IGroup | undefined
  addGroup: (values: IGroupRequest) => void
  updateGroup: (values: IGroupRequest, groupId: string | undefined) => void
  deleteGroup: (groupId: string | undefined) => void
}