import { IAuthContext } from "../../contexts/interfaces";
import { Permission } from "../../enums";
import { IPreferences, IUserRequest } from "../../models";

export interface IAuthService extends IAuthContext {
  login: (user: IUserRequest) => void
  signup: (user: IUserRequest) => void
  logout: () => void
  hasPermission: (applicationId: number, permission: Permission) => boolean
  updatePreferences: (values: IPreferences) => void
}