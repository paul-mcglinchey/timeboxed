import { createContext, useContext, useEffect, useState } from "react";
import { IChildrenProps, IFetch, IMetaInfo, IPermission, IRole } from "../models";
import { useFetch, useRequestBuilderService } from "../hooks";
import { endpoints } from "../config";
import { IMetaInfoContext } from "./interfaces";
import { GroupContext } from "./GroupContext";

export const MetaInfoContext = createContext<IMetaInfoContext>({
  applications: [],
  roles: [],
  permissions: [],
  getRole: () => undefined,
  getPermission: () => undefined,
  getRolePermissions: () => [],
  isLoading: false,
  error: undefined,
});

export const MetaInfoProvider = ({ children }: IChildrenProps) => {
  const [metaInfo, setMetaInfo] = useState<IMetaInfo>({ applications: [], roles: [], permissions: [] })
  const [isLoading, setIsLoading] = useState<boolean>(true)
  const [error, setError] = useState<any>()

  const { buildRequest } = useRequestBuilderService()
  const { currentGroup } = useContext(GroupContext)
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

  const contextValue = {
    ...metaInfo,
    getRole,
    getPermission,
    getRolePermissions,
    isLoading,
    error,
  }

  return (
    <MetaInfoContext.Provider value={contextValue}>
      {children}
    </MetaInfoContext.Provider>
  )
} 