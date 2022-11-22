import { IApplication, IPermission, IRole } from ".";

export interface IMetaInfo {
  applications: IApplication[]
  roles: IRole[]
  permissions: IPermission[]
}