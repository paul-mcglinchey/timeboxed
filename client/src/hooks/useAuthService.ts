import { Dispatch, SetStateAction, useContext } from "react"
import { useNavigate } from "react-router"
import { IPreferences, IUserRequest } from "../models"
import { AuthContext } from "../contexts/AuthContext"
import { endpoints } from '../config'
import { useRequestBuilderService, useAsyncHandler, useResolutionService } from '.'
import { IAuthService } from "./interfaces"
import { IApiError } from "../models/error.model"

const useAuthService = (setIsLoading: Dispatch<SetStateAction<boolean>>, setError: Dispatch<SetStateAction<IApiError | undefined>>): IAuthService => {
  
  const auth = useContext(AuthContext)
  const { user, setUser } = auth

  const { buildRequest } = useRequestBuilderService()
  const { asyncHandler } = useAsyncHandler(setIsLoading)
  const { handleResolution } = useResolutionService()
  const navigate = useNavigate()

  const login = asyncHandler(async (user: IUserRequest) => {
    const res = await fetch(endpoints.login, buildRequest('POST', undefined, user))
    
    if (!res.ok && res.status < 500) return setError(await res.json())

    const json = await res.json()

    setUser(json)
    navigate('/dashboard', { replace: true })
  })

  const signup = asyncHandler(async (user: IUserRequest) => {
    const res = await fetch(endpoints.signup, buildRequest('POST', undefined, user))
    
    if (!res.ok) throw new Error(await res.json())
    
    const json = await res.json()

    handleResolution(res, json, undefined, undefined, [() => setUser(json), () => navigate('/dashboard', { replace: true })], undefined, false)
  })

  const updatePreferences = asyncHandler(async (values: IPreferences) => {
    if (!user?.id) throw new Error()

    const prevUser = user
    setUser(u => u && ({ ...u, preferences: values }))

    const res = await fetch(endpoints.userpreferences(), buildRequest('PUT', undefined, values))
    const json = await res.json()

    handleResolution(res, json, 'update', 'preferences', [() => setUser(u => u && ({ ...u, preferences: json }))], [() => setUser(prevUser)])
  })

  return { ...auth, signup, login, updatePreferences }
}

export default useAuthService;