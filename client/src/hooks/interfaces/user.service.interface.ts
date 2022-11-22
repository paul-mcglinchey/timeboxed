import { IUserContext } from "../../contexts/interfaces"
import { IGroup, IUser } from "../../models"

export interface IUserService extends IUserContext {
  getUser: (userId: string | undefined) => IUser | undefined
  updateUser: (userId: string | undefined, values: IUser) => void
  userHasRole: (group: IGroup, userId: string | undefined, roleId: string | undefined) => boolean
  userHasPermission: (group: IGroup, userId: string | undefined, permissionId: number | undefined) => boolean
}