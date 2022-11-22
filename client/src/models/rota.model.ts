import { ITracking } from ".";
import { ISchedule } from "./schedule.model";

export interface IRota extends ITracking {
  id: string,
  name: string,
  description: string | null,
  closingHour: number | null,
  schedules: ISchedule[],
  employees: string[],
  locked: boolean,
  colour: string | null
}

export interface IRotaRequest {
  name: string | null,
  description: string | null,
  closingHour: number | null,
  employees: string[],
  locked?: boolean,
  colour: string | null
}

export interface IUpdateRotaEmployeesRequest {
  employees: string[]
}

export interface IRotaResponse {
  rota: IRota
}

export interface IRotasResponse {
  count: number,
  items: IRota[]
}