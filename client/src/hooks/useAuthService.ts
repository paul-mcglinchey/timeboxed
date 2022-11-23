import { useContext } from "react"
import { useNavigate } from "react-router"
import { IPreferences, IUserRequest } from "../models"
import { AuthContext } from "../contexts"
import { endpoints } from '../config'
import { useRequestBuilderService, useAsyncHandler, useResolutionService, useGroupService, useUserService } from '.'
import { Permission } from "../enums"
import { IAuthService } from "./interfaces"

const useAuthService = (): IAuthService => {
  
  const auth = useContext(AuthContext)
  const { user, setUser, setIsLoading } = auth

  const { buildRequest } = useRequestBuilderService()
  const { asyncHandler } = useAsyncHandler(setIsLoading)
  const { handleResolution } = useResolutionService()
  const { currentGroup } = useGroupService()
  const { userHasPermission } = useUserService()
  const navigate = useNavigate()

  const login = asyncHandler(async (user: IUserRequest) => {
    const res = await fetch(endpoints.login, buildRequest('POST', undefined, user))

    if (res.status === 401) throw new Error("Invalid user details")
    if (!res.ok) throw new Error(await res.text())

    const json = await res.json()

    setUser(json)
    navigate('/dashboard', { replace: true })
  })

  const signup = asyncHandler(async (user: IUserRequest) => {
    const res = await fetch(endpoints.signup, buildRequest('POST', undefined, user))
    
    if (!res.ok) throw new Error(await res.text())
    
    const json = await res.json()

    handleResolution(res, json, undefined, undefined, [() => setUser(json), () => navigate('/dashboard', { replace: true })], undefined, false)
  })

  const logout = () => {
    setUser(undefined)

    navigate('/login')
  }

  const hasPermission = (applicationId: number, permission: Permission): boolean => {
    if (!currentGroup?.applications?.includes(applicationId)) return false

    return userHasPermission(currentGroup, user?.id, permission)
  }

  const updatePreferences = asyncHandler(async (values: IPreferences) => {
    if (!user?.id) throw new Error()

    const prevUser = user
    setUser(u => u && ({ ...u, preferences: values }))

    const res = await fetch(endpoints.userpreferences(), buildRequest('PUT', undefined, values))
    const json = await res.json()

    handleResolution(res, json, 'update', 'preferences', [() => setUser(u => u && ({ ...u, preferences: json }))], [() => setUser(prevUser)])
  })

  return { ...auth, signup, login, logout, hasPermission, updatePreferences }
}

export default useAuthService;