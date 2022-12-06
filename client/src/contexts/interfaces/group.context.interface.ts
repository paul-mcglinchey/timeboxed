import { Dispatch, SetStateAction } from "react"
import { IGroup, IGroupUser } from "../../models"
import { ILoadable } from "../../models/loadable.model"

export interface IGroupContext extends ILoadable {
  currentGroup: IGroup | undefined
  setCurrentGroupId: Dispatch<SetStateAction<string | undefined>>
  groups: IGroup[]
  setGroups: Dispatch<SetStateAction<IGroup[]>>
  count: number
  setCount: Dispatch<SetStateAction<number>>
  getGroupUser: (userId: string, groupId?: string | undefined) => IGroupUser | undefined
  getPermissions: (groupId: string, userId: string) => number[]
  userHasPermission: (groupId: string, userId: string | undefined, permissionId: number | undefined) => boolean
  userHasRole: (groupId: string, userId: string | undefined, roleId: string | undefined) => boolean
}