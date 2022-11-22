import { IApplication } from "../../models";

export interface IApplicationService {
  applications: IApplication[]
  getApplication: (identifier: number | undefined) => IApplication | undefined
}