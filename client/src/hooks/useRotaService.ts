import { Dispatch, SetStateAction, useContext, useMemo } from "react"
import { IRota, IRotaRequest, IUpdateRotaEmployeesRequest } from "../models"
import { IRotaService } from "./interfaces"
import { GroupContext } from "../contexts/GroupContext"
import { RotaContext } from "../contexts/RotaContext"
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
  const { asyncHandler, asyncReturnHandler } = useAsyncHandler(setIsLoading)
  const { handleResolution } = useResolutionService()
  const groupId: string | undefined = currentGroup?.id

  const fetchRota = useMemo(() => asyncReturnHandler<IRota | undefined>(async (rotaId: string) => {
    if (!groupId) return

    const res = await fetch(endpoints.rota(rotaId, groupId), buildRequest())
    
    if (!res.ok) {
      if (res.status < 500) return setError(await res.json())

      throw new Error()
    }

    return await res.json()
  }), [asyncReturnHandler, buildRequest, groupId, setError])

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
    
    if (!res.ok) {
      if (res.status < 500) return setError(await res.json())

      throw new Error()
    } else {
      updateRotaInContext(rotaId, values)
    }
  })

  const updateRotaEmployees = asyncHandler(async (rotaId: string, values: IUpdateRotaEmployeesRequest) => {
    if (!groupId) throw new Error()

    const res = await fetch(endpoints.rota(rotaId, groupId), buildRequest('PUT', undefined, values))

    if (!res.ok) {
      if (res.status < 500) return setError(await res.json())

      throw new Error()
    } else {
      updateRotaEmployeesInContext(rotaId, values)
    }
    
    if (!res.ok && res.status < 500) return setError(await res.json())
  })

  const deleteRota = asyncHandler(async (rotaId: string) => {
    if (!groupId) throw new Error()

    const res = await fetch(endpoints.rota(rotaId, groupId), buildRequest("DELETE"))
    
    if (!res.ok) {
      if (res.status < 500) return setError(await res.json())

      throw new Error()
    } else {
      deleteRotaInContext(rotaId)
      navigate('/rotas/dashboard')
    }
  })

  const lockRota = asyncHandler(async (rotaId: string) => {
    if (!groupId) throw new Error()

    const res = await fetch(endpoints.lockrota(rotaId, groupId), buildRequest('PUT'))

    if (!res.ok) {
      if (res.status < 500) return setError(await res.json())

      throw new Error()
    } else {
      setRotas(rotas => rotas.map(r => r.id === rotaId ? { ...r, locked: true } : r))
    }
  })

  const unlockRota = asyncHandler(async (rotaId: string) => {
    if (!groupId) throw new Error()

    const res = await fetch(endpoints.unlockrota(rotaId, groupId), buildRequest('PUT'))

    if (!res.ok) {
      if (res.status < 500) return setError(await res.json())

      throw new Error()
    } else {
      setRotas(rotas => rotas.map(r => r.id === rotaId ? { ...r, locked: false } : r))
    }
  })

  const addRotaInContext = (rota: IRota) => {
    setRotas(rotas => [...rotas, rota])
  }

  const updateRotaInContext = (rotaId: string, values: IRotaRequest) => {
    setRotas(rotas => rotas.map(r => r.id === rotaId ? { ...r, ...values } : r ))
  }

  const updateRotaEmployeesInContext = (rotaId: string, values: IUpdateRotaEmployeesRequest) => {
    setRotas(rotas => rotas.map(r => r.id === rotaId ? { ...r, ...values }: r ))
  }

  const deleteRotaInContext = (rotaId: string) => {
    setRotas(rotas => rotas.filter(r => r.id !== rotaId))
  }

  return { ...rotaContext, fetchRota, addRota, updateRota, updateRotaEmployees, deleteRota, lockRota, unlockRota }
}

export default useRotaService