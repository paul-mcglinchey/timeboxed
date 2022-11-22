import { IAudit } from "./audit.model"
import { IPermission } from "./permission.model"

export interface IUserRequest {
  usernameOrEmail?: string
  username?: string
  email?: string
  password: string
}

export interface IPreferences {
  defaultGroup?: string | undefined
}

export interface IUser {
  id: string
  username?: string,
  email?: string,
  password?: string
  token?: string
  isAdmin?: boolean
  invites?: {
    groupId: string
    permissions: IPermission[]
    createdAt?: string
    updatedAt?: string
    audit: IAudit
  }[]
  preferences?: IPreferences
  createdAt?: string
  updatedAt?: string
}

export interface IUsersResponse {
  items: IUser[]
  count: number,
}