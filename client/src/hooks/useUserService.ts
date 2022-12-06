import { useState } from "react"
import { IGroup, IGroupUser } from "../models"
import { IUserService } from "./interfaces"
import { useRoleService } from "."
import { IApiError } from "../models/error.model"

const useUserService = (): IUserService => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()

  const { getRole } = useRoleService()

  const userHasRole = (groupUser: IGroupUser, roleId: string | undefined): boolean => {
    if (!roleId) return false

    return groupUser.roles.includes(roleId) ?? false
  }

  const userHasPermission = (group: IGroup, userId: string | undefined, permissionId: number | undefined): boolean => {
    if (!userId || !permissionId) return false

    return getPermissions(group, userId).includes(permissionId)
  }

  const getPermissions = (group: IGroup, userId: string): number[] => {
    return group.users.find(gu => gu.userId === userId)?.roles.map(r => getRole(r)?.permissions).filter((p): p is number[] => !!p).flatMap(p => p) || []
  }

  return { userHasRole, userHasPermission, isLoading, setIsLoading, error, setError }
}

export default useUserService