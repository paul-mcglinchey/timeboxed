import { Dispatch, SetStateAction } from "react"
import { IGroup, IGroupList } from "../../models"
import { ILoadable } from "../../models/loadable.model"

export interface IGroupContext extends ILoadable {
  currentGroup: IGroup | undefined
  setCurrentGroupId: Dispatch<SetStateAction<string | undefined>>
  groups: IGroupList[]
  setGroups: Dispatch<SetStateAction<IGroupList[]>>
  count: number
  setCount: Dispatch<SetStateAction<number>>
}