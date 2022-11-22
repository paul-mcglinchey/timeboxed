import { IPermission } from "../models"
import { useContext } from "react"
import { MetaInfoContext } from "../contexts"
import { IPermissionService } from "./interfaces"

const usePermissionService = (): IPermissionService => {
  const metaInfoContext = useContext(MetaInfoContext)
  const { permissions = [] } = metaInfoContext

  const getPermission = (permissionId: number | undefined): IPermission | undefined => {
    return permissions.find(p => p.id === permissionId)
  }

  return { permissions, getPermission }
}

export default usePermissionService