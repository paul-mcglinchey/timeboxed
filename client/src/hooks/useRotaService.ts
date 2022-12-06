import { Dispatch, SetStateAction, useContext } from "react"
import { IRota, IRotaRequest, IUpdateRotaEmployeesRequest } from "../models"
import { IRotaService } from "./interfaces"
import { GroupContext, RotaContext } from "../contexts"
import { endpoints } from "../config"
import { useNavigate } from "react-router"
import { useAsyncHandler, useResolutionService, useRequestBuilderService } from '.'
import { IApiError } from "../models/error.model"

const useRotaService = (setIsLoading: Dispatch<SetStateAction<boolean>>, setError: Dispatch<SetStateAction<IApiError | undefined>>): IRotaService => {
  
  const rotaContext = useContext(RotaContext)
  const { setRotas } = rotaContext
  const { currentGroup } = useContext(GroupContext)
  
  const navigate = useNavigate();
  const { buildRequest } = useRequestBuilderService()
  const { asyncHandler } = useAsyncHandler(setIsLoading)
  const { handleResolution } = useResolutionService()
  const groupId: string | undefined = currentGroup?.id

  const addRota = asyncHandler(async (values: IRotaRequest) => {
    if (!groupId) throw new Error()

    const res = await fetch(endpoints.rotas(groupId), buildRequest('POST', undefined, values))
    if (!res.ok && res.status < 500) return setError(await res.json())
    const json: IRota = await res.json()

    handleResolution(res, json, 'create', 'rota', [() => addRotaInContext(json)])
  })

  const updateRota = asyncHandler(async (rotaId: string, values: IRotaRequest) => {    
    if (!groupId) throw new Error()

    const res = await fetch(endpoints.rota(rotaId, groupId), buildRequest('PUT', undefined, values))
    if (!res.ok && res.status < 500) return setError(await res.json())
    const json: IRota = await res.json()

    handleResolution(res, json, 'update', 'rota', [() => updateRotaInContext(rotaId, json)])
  })

  const updateRotaEmployees = asyncHandler(async (rotaId: string, values: IUpdateRotaEmployeesRequest) => {
    if (!groupId) throw new Error()

    const res = await fetch(endpoints.rota(rotaId, groupId), buildRequest('PUT', undefined, values))
    if (!res.ok && res.status < 500) return setError(await res.json())
    const json: IRota = await res.json()

    handleResolution(res, json, 'update', 'rota employees', [() => updateRotaInContext(rotaId, json)])
  })

  const deleteRota = asyncHandler(async (rotaId: string) => {
    if (!groupId) throw new Error()

    const res = await fetch(endpoints.rota(rotaId, groupId), buildRequest("DELETE"))
    if (!res.ok && res.status < 500) return setError(await res.json())
    const json = await res.json()

    handleResolution(res, json, 'delete', 'rota', [() => deleteRotaInContext(rotaId), () => navigate('/rotas/dashboard')])
  })

  const addRotaInContext = (rota: IRota) => {
    setRotas(rotas => [...rotas, rota])
  }

  const updateRotaInContext = (rotaId: string, values: IRota) => {
    setRotas(rotas => rotas.map(r => r.id === rotaId ? { ...r, ...values } : r ))
  }

  const deleteRotaInContext = (rotaId: string) => {
    setRotas(rotas => rotas.filter(r => r.id !== rotaId))
  }

  return { ...rotaContext, addRota, updateRota, updateRotaEmployees, deleteRota }
}

export default useRotaService