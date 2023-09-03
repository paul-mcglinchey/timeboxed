import { Dispatch, SetStateAction } from "react"
import { IFilter, ISortable, ILoadable, IPageable, IClientListResponse } from '../../models'

export interface IClientContext extends ISortable, ILoadable, IPageable {
  clients: IClientListResponse[]
  setClients: Dispatch<SetStateAction<IClientListResponse[]>>
  fetchClients: () => Promise<void>
  count: number
  setCount: Dispatch<SetStateAction<number>>
  filter: IFilter,
  setFilter: Dispatch<SetStateAction<IFilter>>,
  buildQueryString: () => void
  error: any | undefined
}