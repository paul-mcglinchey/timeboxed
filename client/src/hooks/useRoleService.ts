import { useContext } from "react"
import { MetaInfoContext } from "../contexts"
import { IPermission, IRole } from "../models"
import { IRoleService } from "./interfaces"
import { usePermissionService } from '.'

const useRoleService = (): IRoleService => {

  const { getPermission } = usePermissionService()

  const metaInfoContext = useContext(MetaInfoContext)
  const { roles = [] } = metaInfoContext

  const getRole = (roleId: string): IRole | undefined => {
    return roles.find(r => r.id === roleId)
  }

  const getRolePermissions = (roleId: string): IPermission[] => {
    return roles.find(r => r.id === roleId)?.permissions.map(p => getPermission(p)).filter((p): p is IPermission => !!p) || []
  }

  return { roles, getRole, getRolePermissions }
}

export default useRoleService