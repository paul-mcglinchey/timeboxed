import { IPermission } from "../../models"

export interface IPermissionService {
  permissions: IPermission[]
  getPermission: (permissionIdentifer: number | undefined) => IPermission | undefined
}