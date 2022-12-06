import { createContext, useEffect, useState } from "react";
import { IChildrenProps, IGroup, IGroupList } from "../models";
import { useAsyncHandler, useAuthService, useIsMounted, useRequestBuilderService } from "../hooks";
import { endpoints } from "../config";
import { getItemInLocalStorage, setItemInLocalStorage } from "../services";
import { IGroupContext } from "./interfaces";
import { IListResponse } from "../models/list-response.model";

export const GroupContext = createContext<IGroupContext>({
  currentGroup: undefined,
  setCurrentGroupId: () => {},
  groups: [],
  setGroups: () => {},
  count: 0,
  setCount: () => {},
  isLoading: false,
  setIsLoading: () => {},
  error: undefined,
  setError: () => {}
});

export const GroupProvider = ({ children }: IChildrenProps) => {
  const [groups, setGroups] = useState<IGroupList[]>([])
  const [count, setCount] = useState<number>(0)
  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<any>()
  const [currentGroupId, setCurrentGroupId] = useState<string | undefined>(getItemInLocalStorage('group-id'))
  const [currentGroup, setCurrentGroup] = useState<IGroup | undefined>()
  
  const { buildRequest } = useRequestBuilderService()
  const { user } = useAuthService()
  const { asyncHandler } = useAsyncHandler(setIsLoading)
  const isMounted = useIsMounted()
  
  useEffect(() => {
    const _fetch = asyncHandler(async () => {
      if (currentGroupId) {
        var res = await fetch(endpoints.group(currentGroupId), buildRequest())
        var json: IGroup = await res.json()

        setCurrentGroup(json)
      }
    })

    if (isMounted()) {
      _fetch()
    }
  }, [currentGroupId, user, asyncHandler, buildRequest, isMounted])

  useEffect(() => {
    const _fetch = asyncHandler(async () => {
      var res = await fetch(endpoints.groups, buildRequest())
      var json: IListResponse<IGroupList> = await res.json()

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
    if (!currentGroup && groups.length > 0) setCurrentGroupId(groups[0]?.id)
  }, [groups, currentGroup])

  const contextValue = {
    currentGroup,
    setCurrentGroupId,
    groups,
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