import { Dispatch, SetStateAction, useContext } from "react";
import { ISchedule, IScheduleShift, IScheduleRequest } from "../models"
import { IScheduleService } from "./interfaces";
import { GroupContext } from "../contexts/GroupContext";
import { ScheduleContext } from "../contexts/ScheduleContext";
import { endpoints } from '../config'
import { addDays, subDays } from "date-fns";
import { useAsyncHandler, useResolutionService, useRequestBuilderService } from "../hooks";
import { IApiError } from "../models/error.model";

const useScheduleService = (setIsLoading: Dispatch<SetStateAction<boolean>>, setError: Dispatch<SetStateAction<IApiError | undefined>>): IScheduleService => {
  
  const scheduleContext = useContext(ScheduleContext)
  const { getSchedules, setSchedules } = scheduleContext
  const { currentGroup } = useContext(GroupContext)
  
  const { asyncReturnHandler } = useAsyncHandler(setIsLoading)
  const { buildRequest } = useRequestBuilderService()
  const { handleResolution } = useResolutionService()

  const updateSchedule = asyncReturnHandler<ISchedule | void>(async (values: IScheduleRequest, scheduleId: string,  rotaId: string): Promise<ISchedule | void> => {
    if (!currentGroup) throw new Error("Group not set")

    const res = await fetch(endpoints.schedule(rotaId, currentGroup.id, scheduleId), buildRequest("PUT", undefined, values))
    if (!res.ok && res.status < 500) return setError(await res.json())
    const json: ISchedule = await res.json()

    handleResolution(res, json, 'update', 'schedule', [() => updateSchedulesInContext(json, scheduleId)])

    return json
  })

  const createSchedule = asyncReturnHandler<ISchedule | void>(async (values: IScheduleRequest, rotaId: string): Promise<ISchedule | void> => {
    if (!currentGroup?.id) throw new Error()

    const res = await fetch(endpoints.schedules(rotaId, currentGroup.id), buildRequest('POST', undefined, values))
    if (!res.ok && res.status < 500) return setError(await res.json())
    const json: ISchedule = await res.json()

    handleResolution(res, json, 'create', 'schedule', [() => updateSchedulesInContext(json)])

    return json
  })

  const updateSchedulesInContext = (values: ISchedule, scheduleId?: string) => {
    const schedules = getSchedules()
    
    if (scheduleId) {
      schedules[schedules.findIndex(s => s.id === scheduleId)] = values
    } else {
      schedules.push(values)
    }

    setSchedules(schedules)
  }

  const getWeek = (weekModifier: number): { first: Date, last: Date, week: Date[] } => {
    const current = new Date();
    const today = new Date(Date.UTC(current.getUTCFullYear(), current.getUTCMonth(), current.getUTCDate()))
    today.setDate(today.getDate() + (weekModifier * 7));

    // Using custom index here to ensure that the current week is not calculated off of Sunday being the first day
    const mondayAsFirstDayIndex = [6, 0, 1, 2, 3, 4, 5];

    const first = subDays(today, (mondayAsFirstDayIndex[today.getDay()] || 0))
    const last = addDays(first, 6)

    const week: Date[] = mondayAsFirstDayIndex.map((_d, i) => addDays(first, i))

    return { first, last, week };
  }

  const getShift = (shifts: IScheduleShift[], date: Date): IScheduleShift | undefined => {
    const dateOnly = date.toISOString().split("T")[0]

    return shifts.find(s => s.date === dateOnly)
  }

  return { ...scheduleContext, getWeek, updateSchedule, createSchedule, getShift }
}

export default useScheduleService