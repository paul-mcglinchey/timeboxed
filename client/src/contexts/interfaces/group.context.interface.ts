import { Dispatch, SetStateAction } from "react"
import { IGroup } from "../../models"
import { ILoadable } from "../../models/loadable.model"

export interface IGroupContext extends ILoadable {
  currentGroup: IGroup | undefined
  setCurrentGroup: Dispatch<SetStateAction<IGroup | undefined>>
  groups: IGroup[]
  invites: IGroup[]
  setGroups: Dispatch<SetStateAction<IGroup[]>>
  getGroup: (groupId: string | undefined) => IGroup | undefined
  count: number
  setCount: Dispatch<SetStateAction<number>>
  error: any | undefined
}