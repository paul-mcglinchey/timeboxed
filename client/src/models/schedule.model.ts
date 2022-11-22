import { IRota } from "./rota.model";

export interface IScheduleShift {
  id: string
  date: string
  startHour: string | null
  endHour: string | null
  notes: string
}

export interface IScheduleShiftRequest {
  date: string
  startHour: string | null
  endHour: string | null
  notes: string | null
}

export interface IEmployeeSchedule {
  id: string
  employeeId: string,
  shifts: IScheduleShift[]
}

export interface IEmployeeScheduleRequest {
  employeeId: string
  shifts: IScheduleShiftRequest[]
}

export interface ISchedule {
  id: string,
  startDate: string,
  locked: boolean,
  employeeSchedules: IEmployeeSchedule[]
}

export interface IScheduleRequest {
  locked?: boolean
  employeeSchedules: IEmployeeScheduleRequest[]
}

export interface IScheduleResponse {
  rota: IRota
  schedule: ISchedule
}

export interface ISchedulesResponse {
  count: number
  items: ISchedule[]
}