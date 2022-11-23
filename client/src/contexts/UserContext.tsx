import { createContext, useEffect, useState } from "react";
import { endpoints } from "../config";
import { useGroupService, useIsMounted, useRequestBuilderService } from "../hooks";
import { IChildrenProps, IUser, IUsersResponse } from "../models";
import { IUserContext } from "./interfaces";

export const UserContext = createContext<IUserContext>({
  users: [],
  setUsers: () => {},
  count: 0,
  setCount: () => {},
  isLoading: false,
  setIsLoading: () => {},
  error: undefined,
  setError: () => {}
});

export const UserProvider = ({ children }: IChildrenProps) => {
  const [users, setUsers] = useState<IUser[]>([])
  const [count, setCount] = useState<number>(0)
  const [isLoading, setIsLoading] = useState<boolean>(true)
  const [error, setError] = useState<any>()

  const { buildRequest } = useRequestBuilderService()
  const { currentGroup } = useGroupService()
  const isMounted = useIsMounted()

  useEffect(() => {
    if (isMounted() && currentGroup) {
      const _fetch = async () => {
        setIsLoading(true)

        const res = await fetch(endpoints.groupusers(currentGroup.id), buildRequest())
        const json: IUsersResponse = await res.json()

        setIsLoading(false)
        
        setUsers(json.items)
        setCount(json.count)
      }

      _fetch()
    }
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [currentGroup])

  const contextValue = {
    users,
    setUsers,
    count,
    setCount,
    isLoading,
    setIsLoading,
    error,
    setError
  }

  return (
    <UserContext.Provider value={contextValue}>
      {children}
    </UserContext.Provider>
  )
} 