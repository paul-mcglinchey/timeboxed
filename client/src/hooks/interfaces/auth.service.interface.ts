import { IAuthContext } from "../../contexts/interfaces";
import { IPreferences, IUserRequest } from "../../models";

export interface IAuthService extends IAuthContext {
  login: (user: IUserRequest) => void
  signup: (user: IUserRequest) => void
  logout: () => void
  updatePreferences: (values: IPreferences) => void
}