import { IUser } from "."

export interface IGroupUserApplication {
  applicationId: number
  roles: string[]
}

export interface IGroupUser {
  id: string
  groupId: string
  userId: string
  hasJoined: boolean
  applications: number[]
  roles: string[]
}

export interface IGroup {
  id: string
  name: string
  description: string | null
  applications: number[]
  groupUsers: IGroupUser[]
  listDefinitions?: string
  colour: string | null
}
export interface IGroupRequest {
  name: string
  description: string
  applications: number[]
  colour: string
}

export interface IGroupUserRequest {
  roles: string[]
  applications: number[]
}

export interface IGroupUserInviteRequest {
  usernameOrEmail: string
}

export interface IGroupsResponse {
  count: number,
  items: IGroup[]
}

export interface IMappableGroupUser {
  gu: IGroupUser
  user: IUser | undefined
}