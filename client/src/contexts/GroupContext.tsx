import { createContext, useEffect, useState } from "react";
import { IChildrenProps, IGroup, IGroupsResponse } from "../models";
import { useAsyncHandler, useAuthService, useIsMounted, useRequestBuilderService } from "../hooks";
import { endpoints } from "../config";
import { getItemInLocalStorage, setItemInLocalStorage } from "../services";
import { IGroupContext } from "./interfaces";

export const GroupContext = createContext<IGroupContext>({
  currentGroup: undefined,
  setCurrentGroup: () => {},
  groups: [],
  invites: [],
  setGroups: () => {},
  count: 0,
  setCount: () => {},
  isLoading: false,
  setIsLoading: () => {},
  error: undefined,
  setError: () => {}
});

export const GroupProvider = ({ children }: IChildrenProps) => {
  const [groups, setGroups] = useState<IGroup[]>([])
  const [count, setCount] = useState<number>(0)
  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<any>()
  const [currentGroup, setCurrentGroup] = useState<IGroup | undefined>(groups && groups.find(g => g.id === getItemInLocalStorage('group-id')))
  
  const { buildRequest } = useRequestBuilderService()
  const { user } = useAuthService()
  const { asyncHandler } = useAsyncHandler(setIsLoading)
  const isMounted = useIsMounted()
  
  useEffect(() => {
    const _fetch = asyncHandler(async () => {
      var res = await fetch(endpoints.groups, buildRequest())
      var json: IGroupsResponse = await res.json()

      setGroups(json.items)
      setCount(json.count)
    })
    
    if (isMounted()) {
      _fetch()
    }
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [user])

  useEffect(() => {
    currentGroup && setItemInLocalStorage('group-id', currentGroup?.id)
  }, [currentGroup])

  useEffect(() => {
    setCurrentGroup(groups.find(g => g.id === getItemInLocalStorage('group-id')) || groups[0])
  }, [groups])

  const contextValue = {
    currentGroup,
    setCurrentGroup,
    groups: groups?.filter(g => g.groupUsers.find(gu => gu.userId === user?.id)?.hasJoined),
    invites: groups?.filter(g => !g.groupUsers.find(gu => gu.userId === user?.id)?.hasJoined),
    setGroups,
    count,
    setCount,
    isLoading,
    setIsLoading,
    error,
    setError
  }

  return (
    <GroupContext.Provider value={contextValue}>
      {children}
    </GroupContext.Provider>
  )
} 