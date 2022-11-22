import { Dispatch, SetStateAction } from "react"
import { ILoadable, IUser } from "../../models"

export interface IAuthContext extends ILoadable {
  user: IUser | undefined
  setUser: Dispatch<SetStateAction<IUser | undefined>>
  getAccess: () => boolean
  getToken: () => string | undefined
  getCookie: () => IUser | undefined
  isAdmin: () => boolean
}