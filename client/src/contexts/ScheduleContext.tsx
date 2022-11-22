import { createContext, useCallback, useEffect, useState } from "react";
import { IChildrenProps, IFetch, ISchedule, ISchedulesResponse } from "../models";
import { useFetch, useGroupService, useRequestBuilder } from "../hooks";
import { endpoints } from "../config";
import { IScheduleContext } from "./interfaces";

interface IScheduleProviderProps {
  rotaId: string | undefined
}

export const ScheduleContext = createContext<IScheduleContext>({
  getSchedules: () => [],
  setSchedules: () => {},
  getCount: () => 0,
  isLoading: false,
  setIsLoading: () => {},
  error: undefined,
  setError: () => {}
});

export const ScheduleProvider = ({ rotaId, children }: IScheduleProviderProps & IChildrenProps) => {
  const [schedules, setSchedules] = useState<ISchedule[]>([])
  const [count, setCount] = useState<number>(0)
  const [isLoading, setIsLoading] = useState<boolean>(true)
  const [error, setError] = useState<any>()

  const { currentGroup } = useGroupService()

  const { requestBuilder } = useRequestBuilder()
  const { response }: IFetch<ISchedulesResponse> = useFetch(endpoints.schedules(rotaId || "", currentGroup?.id || ""), requestBuilder(), [rotaId, currentGroup], setIsLoading, setError)

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