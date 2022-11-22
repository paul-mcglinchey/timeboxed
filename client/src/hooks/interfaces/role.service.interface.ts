import { IPermission, IRole } from "../../models"

export interface IRoleService {
  roles: IRole[]
  getRole: (roleId: string) => IRole | undefined
  getRolePermissions: (roleId: string) => IPermission[]
}