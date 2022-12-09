import { ITracking } from ".";
import { ISchedule } from "./schedule.model";

export interface IRota extends ITracking {
  id: string,
  name: string,
  description: string,
  closingHour: number,
  schedules: ISchedule[],
  employees: string[],
  locked: boolean,
  colour: string
}

export interface IRotaRequest {
  name: string,
  description: string,
  closingHour: number,
  employees: string[],
  locked?: boolean,
  colour: string
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