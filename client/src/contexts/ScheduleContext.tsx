import { createContext, useCallback, useContext, useEffect, useState } from "react";
import { IChildrenProps, IFetch, ISchedule, ISchedulesResponse } from "../models";
import { useFetch, useRequestBuilderService } from "../hooks";
import { endpoints } from "../config";
import { IScheduleContext } from "./interfaces";
import { GroupContext } from "./GroupContext";

interface IScheduleProviderProps {
  rotaId: string | undefined
}

export const ScheduleContext = createContext<IScheduleContext>({
  getSchedules: () => [],
  setSchedules: () => {},
  getCount: () => 0,
  isLoading: false,
  error: undefined,
});

export const ScheduleProvider = ({ rotaId, children }: IScheduleProviderProps & IChildrenProps) => {
  const [schedules, setSchedules] = useState<ISchedule[]>([])
  const [count, setCount] = useState<number>(0)
  const [isLoading, setIsLoading] = useState<boolean>(true)
  const [error, setError] = useState<any>()

  const { currentGroup } = useContext(GroupContext)

  const { buildRequest } = useRequestBuilderService()
  const { response }: IFetch<ISchedulesResponse> = useFetch(endpoints.schedules(rotaId || "", currentGroup?.id || ""), buildRequest(), [rotaId, currentGroup], setIsLoading, setError)

  useEffect(() => {
    if (response) {
      setSchedules(response.items)
      setCount(response.count)
    }
  }, [response])

  const contextValue = {
    getSchedules: useCallback(() => schedules, [schedules]),
    setSchedules,
    getCount: useCallback(() => count, [count]),
    isLoading,
    setIsLoading,
    error,
    setError
  }

  return (
    <ScheduleContext.Provider value={contextValue}>
      {children}
    </ScheduleContext.Provider>
  )
} 