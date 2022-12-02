import { IUserContext } from "../../contexts/interfaces"
import { IGroup, IGroupUser, IUser } from "../../models"

export interface IUserService extends IUserContext {
  getUser: (userId: string | undefined) => IUser | undefined
  updateUser: (userId: string | undefined, values: IUser) => void
  userHasRole: (groupUser: IGroupUser, roleId: string | undefined) => boolean
  userHasPermission: (group: IGroup, userId: string | undefined, permissionId: number | undefined) => boolean
}