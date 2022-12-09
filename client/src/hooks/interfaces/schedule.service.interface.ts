import { IScheduleContext } from "../../contexts/interfaces";
import { ISchedule, IScheduleShift, IScheduleRequest } from "../../models";

export interface IScheduleService extends IScheduleContext {
  createSchedule: (values: IScheduleRequest, rotaId: string) => Promise<ISchedule | void>
  updateSchedule: (values: IScheduleRequest, rotaId: string, scheduleId: string) => Promise<ISchedule | void>
  getWeek: (weekModifier: number) => { first: Date, last: Date, week: Date[] }
  getShift: (shifts: IScheduleShift[], date: Date) => IScheduleShift | undefined
}