import { Dispatch, SetStateAction } from "react"
import { IClient, IFilter, ISortable, ILoadable, IPageable } from '../../models'

export interface IClientContext extends ISortable, ILoadable, IPageable {
  clients: IClient[]
  setClients: Dispatch<SetStateAction<IClient[]>>
  count: number
  setCount: Dispatch<SetStateAction<number>>
  filters: IFilter,
  setFilters: (filters: IFilter) => void,
  error: any | undefined
}