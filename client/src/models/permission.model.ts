import { IApplication } from "."

export interface IPermission {
  id: number
  name: string,
  description: string
  language?: string,
  applications: IApplication[]
}

export interface IPermissionsResponse {
  count: number
  items: IPermission[]
}