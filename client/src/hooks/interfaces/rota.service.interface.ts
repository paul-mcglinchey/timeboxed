import { IRotaContext } from "../../contexts/interfaces"
import { IRota, IRotaRequest, IUpdateRotaEmployeesRequest } from "../../models"

export interface IRotaService extends IRotaContext {
  getRota: (rotaId: string) => IRota | undefined
  addRota: (values: IRotaRequest) => void
  updateRota: (rotaId: string, values: IRotaRequest) => void
  updateRotaEmployees: (rotaId: string, values: IUpdateRotaEmployeesRequest) => void
  deleteRota: (rotaId: string) => void
}