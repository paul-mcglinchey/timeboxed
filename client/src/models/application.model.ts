import { IAudit } from "."

export interface IApplication {
  id: number
  name?: string
  description?: string
  icon?: string
  backgroundImage?: string
  backgroundVideo?: string
  url?: string
  colour?: string
  audit?: IAudit
}

export interface IApplicationsResponse {
  items: IApplication[]
  count: number
}