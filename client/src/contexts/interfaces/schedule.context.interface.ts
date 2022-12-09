import { Dispatch, SetStateAction } from "react"
import { ISchedule } from "../../models"
import { ILoadable } from "../../models/loadable.model"

export interface IScheduleContext extends ILoadable {
  getSchedules: () => ISchedule[]
  setSchedules: Dispatch<SetStateAction<ISchedule[]>>
  getCount: () => number
  getSchedule: (date: Date) => ISchedule | undefined
}