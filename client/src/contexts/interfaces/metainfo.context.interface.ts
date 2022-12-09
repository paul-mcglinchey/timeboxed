import { Permission } from "../../enums"
import { IApplication, IPermission, IRole } from "../../models"
import { ILoadable } from "../../models/loadable.model"

export interface IMetaInfoContext extends ILoadable {
  applications: IApplication[]
  roles: IRole[]
  permissions: IPermission[]
  getRole: (roleId: string) => IRole | undefined
  getPermission: (permissionId: number) => IPermission | undefined
  getRolePermissions: (roleId: string) => IPermission[]
  getPermissions: (groupId: string, userId: string) => number[]
  userHasPermission: (groupId: string, userId: string | undefined, permissionId: number | undefined) => boolean
  hasPermission: (applicationId: number, permission: Permission) => boolean
}