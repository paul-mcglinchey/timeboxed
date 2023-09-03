import { Dispatch, SetStateAction } from "react"
import { IGroup, IGroupUser } from "../../models"
import { ILoadable } from "../../models/loadable.model"

export interface IGroupContext extends ILoadable {
  fetchGroups: () => Promise<void>
  currentGroup: IGroup | undefined
  setCurrentGroupId: Dispatch<SetStateAction<string | undefined>>
  groups: IGroup[]
  setGroups: Dispatch<SetStateAction<IGroup[]>>
  count: number
  setCount: Dispatch<SetStateAction<number>>
  getGroup: (groupId: string) => IGroup | undefined
  getGroupUser: (userId: string, groupId?: string | undefined) => IGroupUser | undefined
  userHasRole: (groupId: string, userId: string | undefined, roleId: string | undefined) => boolean
}