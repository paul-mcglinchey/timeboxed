import { createContext, useContext, useEffect, useState } from "react";
import { IChildrenProps, IFetch, IMetaInfo, IPermission, IRole } from "../models";
import { useFetch, useGroupService, useRequestBuilderService } from "../hooks";
import { endpoints } from "../config";
import { IMetaInfoContext } from "./interfaces";
import { GroupContext } from "./GroupContext";
import { AuthContext } from "./AuthContext";
import { Permission } from "../enums";

export const MetaInfoContext = createContext<IMetaInfoContext>({
  applications: [],
  roles: [],
  permissions: [],
  getRole: () => undefined,
  getPermission: () => undefined,
  getRolePermissions: () => [],
  getPermissions: () => [],
  userHasPermission: () => false,
  hasPermission: () => false,
  isLoading: false,
  error: undefined,
});

export const MetaInfoProvider = ({ children }: IChildrenProps) => {
  const [metaInfo, setMetaInfo] = useState<IMetaInfo>({ applications: [], roles: [], permissions: [] })
  const [isLoading, setIsLoading] = useState<boolean>(true)
  const [error, setError] = useState<any>()

  const { buildRequest } = useRequestBuilderService()
  const { currentGroup } = useContext(GroupContext)
  const { getGroupFromContext } = useGroupService(setIsLoading, setError)
  const { user } = useContext(AuthContext)
  const { response }: IFetch<IMetaInfo> = useFetch(endpoints.metainfo(currentGroup?.id), buildRequest(), [currentGroup], setIsLoading, setError)

  useEffect(() => {
    if (response) {
      setMetaInfo(response)
    }
  }, [response])

  const getRole = (roleId: string): IRole | undefined => {
    return metaInfo.roles.find(r => r.id === roleId)
  }

  const getPermission = (permissionId: number | undefined): IPermission | undefined => {
    return metaInfo.permissions.find(p => p.id === permissionId)
  }

  const getRolePermissions = (roleId: string): IPermission[] => {
    return metaInfo.roles.find(r => r.id === roleId)?.permissions.map(p => getPermission(p)).filter((p): p is IPermission => !!p) || []
  }

  const getPermissions = (groupId: string, userId: string): number[] => {
    return getGroupFromContext(groupId)
      ?.users.find(gu => gu.userId === userId)
        ?.roles.map(r => getRole(r)?.permissions)
          .filter((p): p is number[] => !!p)
          .flatMap(p => p) ?? []
  }

  const userHasPermission = (groupId: string, userId: string | undefined, permissionId: number | undefined): boolean => {
    if (!userId || !permissionId) return false

    return getPermissions(groupId, userId).includes(permissionId)
  }

  const hasPermission = (applicationId: number, permission: Permission): boolean => {
    if (!currentGroup?.applications?.includes(applicationId)) return false

    return userHasPermission(currentGroup.id, user?.id, permission)
  }

  const contextValue = {
    ...metaInfo,
    getRole,
    getPermission,
    getRolePermissions,
    getPermissions,
    userHasPermission,
    hasPermission,
    isLoading,
    error,
  }

  return (
    <MetaInfoContext.Provider value={contextValue}>
      {children}
    </MetaInfoContext.Provider>
  )
} 