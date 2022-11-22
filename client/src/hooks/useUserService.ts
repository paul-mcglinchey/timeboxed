import { useContext } from "react"
import { IGroup, IUser } from "../models"
import { UserContext } from "../contexts"
import { IUserService } from "./interfaces"
import { useAsyncHandler, useRequestBuilder, useResolutionService, useRoleService } from "."
import { endpoints } from "../config"

const useUserService = (): IUserService => {
  const userContext = useContext(UserContext)
  const { users, setUsers, setIsLoading } = userContext

  const { asyncHandler } = useAsyncHandler(setIsLoading)
  const { requestBuilder } = useRequestBuilder()
  const { handleResolution } = useResolutionService()
  const { getRole } = useRoleService()

  const getUser = (userId: string | undefined): IUser | undefined => {
    return users.find((user: IUser) => user.id === userId)
  }

  const updateUser = asyncHandler(async (userId: string | undefined, values: IUser) => {
    if (!userId) throw new Error()

    const res = await fetch(endpoints.user(userId), requestBuilder('PUT', undefined, values))
    const json = await res.json()

    handleResolution(res, json, 'update', 'user', [() => updateUserInContext(userId, values)])
  })

  const updateUserInContext = (userId: string, values: IUser) => {
    setUsers(users => users.map(u => {
      return u.id === userId ? { ...u, ...values } : u
    }))
  }

  const userHasRole = (group: IGroup, userId: string | undefined, roleId: string | undefined): boolean => {
    if (!userId || !roleId) return false

    const gu = group.groupUsers.find(gu => gu.userId === userId)

    return gu?.roles.includes(roleId) ?? false
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