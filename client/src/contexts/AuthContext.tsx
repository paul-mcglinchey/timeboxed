import { createContext, useEffect, useState } from "react";
import { IChildrenProps, IUser } from "../models";
import { useCookies } from 'react-cookie'
import { useAsyncHandler, useRequestBuilderService } from "../hooks";
import { endpoints } from "../config";
import { IAuthContext } from "./interfaces";
import { useNavigate } from "react-router";
import { useLocation } from "react-router";

export const AuthContext = createContext<IAuthContext>({
  user: undefined,
  setUser: () => { },
  getAccess: () => false,
  getToken: () => undefined,
  getCookie: () => undefined,
  isAdmin: () => false,
  isLoading: false,
  setIsLoading: () => {},
  error: undefined,
  setError: () => {}
});

export const AuthProvider = ({ children }: IChildrenProps) => {
  const [cookies, setCookie, removeCookie] = useCookies(['UserAuth'])
  const [user, setUser] = useState<IUser | undefined>(cookies.UserAuth)
  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<any>()

  const { asyncHandler } = useAsyncHandler(setIsLoading, setError);
  const { buildRequest } = useRequestBuilderService()

  const location = useLocation()
  const navigate = useNavigate()
  
  const authenticate = asyncHandler(async () => {
    if (['/login', '/signup', '/passwordreset'].includes(location.pathname)) return
    if (!user) throw new Error('Unable to re-authenticate user')

    const res = await fetch(endpoints.authenticate, buildRequest('GET', user?.token))

    if (!res.ok) throw new Error("Unable to authenticate user")

    const json: IUser = await res.json()
    setUser(json)

  }, [() => navigate('/login'), () => removeCookie('UserAuth')])
  
  useEffect(() => {
    const currentDate = new Date()
    const expiryDate = new Date(currentDate.setDate(currentDate.getDate() + 14))

    user?.token
      ? setCookie('UserAuth', user, { path: '/', expires: expiryDate })
      : removeCookie('UserAuth')
  }, [user, removeCookie, setCookie])

  useEffect(() => {
    authenticate()

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [])

  const getAccess = () => user !== undefined
  const getToken = () => user?.token
  const getCookie = () => cookies.UserAuth
  const isAdmin = () => user?.isAdmin || false

  const contextValue = {
    user,
    setUser,
    getAccess,
    getToken,
    getCookie,
    isAdmin,
    isLoading,
    setIsLoading,
    error,
    setError
  }

  return (
    <AuthContext.Provider value={contextValue}>
      {children}
    </AuthContext.Provider>
  )
} 