import { createContext, useContext, useEffect, useState } from "react";
import { IChildrenProps, IGroup, IGroupUser } from "../models";
import { useAsyncHandler, useIsMounted, useRequestBuilderService } from "../hooks";
import { endpoints } from "../config";
import { getItemInLocalStorage, setItemInLocalStorage } from "../services";
import { IGroupContext } from "./interfaces";
import { IListResponse } from "../models/list-response.model";
import { IApiError } from "../models/error.model";
import { AuthContext } from "./AuthContext";
import { MetaInfoContext } from "./MetaInfoContext";

export const GroupContext = createContext<IGroupContext>({
  currentGroup: undefined,
  setCurrentGroupId: () => {},
  groups: [],
  setGroups: () => {},
  count: 0,
  setCount: () => {},
  getGroupUser: () => undefined,
  getPermissions: () => [],
  userHasPermission: () => false,
  userHasRole: () => false,
  isLoading: false,
  error: undefined
});

export const GroupProvider = ({ children }: IChildrenProps) => {
  const [groups, setGroups] = useState<IGroup[]>([])
  const [count, setCount] = useState<number>(0)
  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()
  const [currentGroupId, setCurrentGroupId] = useState<string | undefined>(getItemInLocalStorage('group-id'))
  
  const currentGroup = currentGroupId ? groups.find(g => g.id === currentGroupId) : groups[0]

  const { user } = useContext(AuthContext)
  const { getRole } = useContext(MetaInfoContext)

  const { buildRequest } = useRequestBuilderService()
  const { asyncHandler } = useAsyncHandler(setIsLoading)
  const isMounted = useIsMounted()

  useEffect(() => {
    const _fetch = asyncHandler(async () => {
      var res = await fetch(endpoints.groups, buildRequest())
      var json: IListResponse<IGroup> = await res.json()

      if (!res.ok && res.status < 500) return setError(await res.json())

      setGroups(json.items)
      setCount(json.count)
    })
    
    if (isMounted()) {
      _fetch()
    }
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [user])

  useEffect(() => {
    currentGroup && setItemInLocalStorage('group-id', currentGroup.id)
  }, [currentGroup])

  const getGroup = (groupId: string): IGroup | undefined => {
    return groups.find(g => g.id === groupId)
  }

  const getGroupUser = (userId: string, groupId?: string | undefined): IGroupUser | undefined => {
    return groups.find(g => g.id === (groupId ?? currentGroupId))?.users.find(u => u.id === userId)
  }

  const getPermissions = (groupId: string, userId: string): number[] => {
    return getGroup(groupId)?.users.find(gu => gu.userId === userId)?.roles.map(r => getRole(r)?.permissions).filter((p): p is number[] => !!p).flatMap(p => p) || []
  }

  const userHasPermission = (groupId: string, userId: string | undefined, permissionId: number | undefined): boolean => {
    if (!userId || !permissionId) return false

    return getPermissions(groupId, userId).includes(permissionId)
  }

  const userHasRole = (groupId: string, userId: string | undefined, roleId: string | undefined): boolean => {
    if (!userId || !roleId) return false

    return getGroupUser(userId, groupId)?.roles.includes(roleId) ?? false
  }

  const contextValue = {
    currentGroup,
    setCurrentGroupId,
    groups,
    setGroups,
    count,
    setCount,
    getGroup,
    getGroupUser,
    getPermissions,
    userHasPermission,
    userHasRole,
    isLoading,
    error
  }

  return (
    <GroupContext.Provider value={contextValue}>
      {children}
    </GroupContext.Provider>
  )
} 