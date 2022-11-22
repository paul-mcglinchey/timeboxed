import { IApplication, IPermission, IRole } from "../../models"
import { ILoadable } from "../../models/loadable.model"

export interface IMetaInfoContext extends ILoadable {
  applications?: IApplication[] | undefined
  roles?: IRole[] | undefined
  permissions?: IPermission[] | undefined
  error: any
}