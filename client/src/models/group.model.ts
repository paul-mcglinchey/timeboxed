export interface IGroupRequest {
  name: string
  description: string
  applications?: number[]
  colour: string
}

export interface IGroupUserRequest {
  roles: string[]
  applications: number[]
}

export interface IGroupUserInviteRequest {
  usernameOrEmail: string
}

export interface IGroupUser {
  id: string
  groupId: string
  userId: string
  username: string
  email: string
  hasJoined: boolean
  applications: number[]
  roles: string[]
}

export interface IGroup {
  id: string
  name: string
  description: string
  applications: number[]
  users: IGroupUser[]
  listDefinitions?: string
  colour: string
}

export interface IGroupList {
  id: string
  name: string
  description: string
  applications: number[]
  users: string[]
  colour: string
}