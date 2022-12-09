import { createContext, useCallback, useContext, useEffect, useState } from "react";
import { IChildrenProps, IFetch, ISchedule, ISchedulesResponse } from "../models";
import { useFetch, useRequestBuilderService } from "../hooks";
import { endpoints } from "../config";
import { IScheduleContext } from "./interfaces";
import { GroupContext } from "./GroupContext";
import { getDateOnly } from "../services";

interface IScheduleProviderProps {
  rotaId: string | undefined
}

export const ScheduleContext = createContext<IScheduleContext>({
  getSchedules: () => [],
  setSchedules: () => {},
  getCount: () => 0,
  getSchedule: () => undefined,
  isLoading: false,
  error: undefined
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

  const getSchedule = (date: Date): ISchedule | undefined => {
    return schedules.find((schedule: ISchedule) => getDateOnly(new Date(schedule.startDate)) === getDateOnly(date))
  }

  const contextValue = {
    getSchedules: useCallback(() => schedules, [schedules]),
    setSchedules,
    getCount: useCallback(() => count, [count]),
    getSchedule,
    isLoading,
    error,
  }

  return (
    <ScheduleContext.Provider value={contextValue}>
      {children}
    </ScheduleContext.Provider>
  )
} 