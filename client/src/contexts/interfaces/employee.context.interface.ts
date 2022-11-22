import { Dispatch, SetStateAction } from "react"
import { IEmployee, ISortable, ILoadable } from "../../models"

export interface IEmployeeContext extends ISortable, ILoadable {
  employees: IEmployee[]
  setEmployees: Dispatch<SetStateAction<IEmployee[]>>
  count: number
  setCount: Dispatch<SetStateAction<number>>
  error: any | undefined
}