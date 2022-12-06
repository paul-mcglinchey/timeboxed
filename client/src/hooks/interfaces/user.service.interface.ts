import { IGroup, IGroupUser, ILoadable } from "../../models"
import { IApiError } from "../../models/error.model"

export interface IUserService extends ILoadable {
  userHasRole: (groupUser: IGroupUser, roleId: string | undefined) => boolean
  userHasPermission: (group: IGroup, userId: string | undefined, permissionId: number | undefined) => boolean
  isLoading: boolean
  error: IApiError | undefined
}