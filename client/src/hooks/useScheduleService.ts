import { useContext } from "react";
import { ISchedule, IScheduleShift, IScheduleRequest } from "../models"
import { IScheduleService } from "./interfaces";
import { ScheduleContext } from "../contexts";
import { endpoints } from '../config'
import { addDays, subDays } from "date-fns";
import { useAsyncHandler, useResolutionService, useGroupService, useRequestBuilder } from "../hooks";
import { getDateOnly } from "../services";

const useScheduleService = (): IScheduleService => {
  
  const scheduleContext = useContext(ScheduleContext)
  const { getSchedules, setSchedules, setIsLoading } = scheduleContext
  
  const { asyncReturnHandler } = useAsyncHandler(setIsLoading)
  const { requestBuilder } = useRequestBuilder()
  const { handleResolution } = useResolutionService()
  
  const { currentGroup } = useGroupService()

  const getSchedule = (date: Date): ISchedule | undefined => getSchedules().find((schedule: ISchedule) => getDateOnly(new Date(schedule.startDate)) === getDateOnly(date))

  const updateSchedule = asyncReturnHandler<ISchedule>(async (values: IScheduleRequest, scheduleId: string,  rotaId: string): Promise<ISchedule> => {
    if (!currentGroup) throw new Error("Group not set")

    const res = await fetch(endpoints.schedule(rotaId, currentGroup.id, scheduleId), requestBuilder("PUT", undefined, values))
    const json: ISchedule = await res.json()

    handleResolution(res, json, 'update', 'schedule', [() => updateSchedulesInContext(json, scheduleId)])

    return json
  })

  const createSchedule = asyncReturnHandler<ISchedule>(async (values: IScheduleRequest, rotaId: string): Promise<ISchedule> => {
    if (!currentGroup?.id) throw new Error()

    const res = await fetch(endpoints.schedules(rotaId, currentGroup.id), requestBuilder('POST', undefined, values))
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
    let current = new Date();
    let today = new Date(Date.UTC(current.getUTCFullYear(), current.getUTCMonth(), current.getUTCDate()))
    today.setDate(today.getDate() + (weekModifier * 7));

    // Using custom index here to ensure that the current week is not calculated off of Sunday being the first day
    let mondayAsFirstDayIndex = [6, 0, 1, 2, 3, 4, 5];

    let first = subDays(today, (mondayAsFirstDayIndex[today.getDay()] || 0))
    let last = addDays(first, 6)

    let week: Date[] = mondayAsFirstDayIndex.map((_d, i) => addDays(first, i))

    return { first, last, week };
  }

  const getShift = (shifts: IScheduleShift[], date: Date): IScheduleShift | undefined => {
    let dateOnly = date.toISOString().split("T")[0]

    return shifts.find(s => s.date === dateOnly)
  }

  return { ...scheduleContext, getSchedule, getWeek, updateSchedule, createSchedule, getShift }
}

export default useScheduleService