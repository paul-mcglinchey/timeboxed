import { Dispatch, SetStateAction } from "react"
import { IUser } from "../../models"
import { ILoadable } from "../../models/loadable.model"

export interface IUserContext extends ILoadable {
  users: IUser[]
  setUsers: Dispatch<SetStateAction<IUser[]>>
  count: number
  setCount: Dispatch<SetStateAction<number>>
  error: any | undefined
  setError: Dispatch<SetStateAction<any | undefined>>
}