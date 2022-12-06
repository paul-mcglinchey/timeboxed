import { Dispatch, SetStateAction } from "react"
import { ILoadable, IRota, ISortable } from '../../models'

export interface IRotaContext extends ILoadable, ISortable {
  rotas: IRota[]
  setRotas: Dispatch<SetStateAction<IRota[]>>
  count: number
  rotaId: string | undefined
  setRotaId: Dispatch<SetStateAction<string | undefined>>
  getRota: (rotaId: string) => IRota | undefined
}