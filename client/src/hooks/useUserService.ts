import { useCallback, useContext } from "react"
import { IGroup, IGroupUser, IUser } from "../models"
import { UserContext } from "../contexts"
import { IUserService } from "./interfaces"
import { useAsyncHandler, useRequestBuilderService, useResolutionService, useRoleService } from "."
import { endpoints } from "../config"

const useUserService = (): IUserService => {
  const userContext = useContext(UserContext)
  const { users, setUsers, setIsLoading, setError } = userContext

  const { asyncHandler } = useAsyncHandler(setIsLoading)
  const { buildRequest } = useRequestBuilderService()
  const { handleResolution } = useResolutionService()
  const { getRole } = useRoleService()

  const getUser = useCallback((userId: string | undefined): IUser | undefined => {
    return users.find((user: IUser) => user.id === userId)
  }, [users])

  const updateUser = asyncHandler(async (userId: string | undefined, values: IUser) => {
    if (!userId) throw new Error()

    const res = await fetch(endpoints.user(userId), buildRequest('PUT', undefined, values))
    if (!res.ok && res.status < 500) return setError(await res.json())
    const json = await res.json()

    handleResolution(res, json, 'update', 'user', [() => updateUserInContext(userId, values)])
  })

  const updateUserInContext = (userId: string, values: IUser) => {
    setUsers(users => users.map(u => {
      return u.id === userId ? { ...u, ...values } : u
    }))
  }

  const userHasRole = (groupUser: IGroupUser, roleId: string | undefined): boolean => {
    if (!roleId) return false

    return groupUser.roles.includes(roleId) ?? false
  }

  const userHasPermission = (group: IGroup, userId: string | undefined, permissionId: number | undefined): boolean => {
    if (!userId || !permissionId) return false

    return getPermissions(group, userId).includes(permissionId)
  }

  const getPermissions = (group: IGroup, userId: string): number[] => {
    return group.groupUsers.find(gu => gu.userId === userId)?.roles.map(r => getRole(r)?.permissions).filter((p): p is number[] => !!p).flatMap(p => p) || []
  }

  return { ...userContext, getUser, updateUser, userHasRole, userHasPermission }
}

export default useUserService