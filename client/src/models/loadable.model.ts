import { Dispatch, SetStateAction } from "react"
import { IApiError } from "./error.model"

export interface ILoadable {
  isLoading: boolean
  error: IApiError | undefined
}



export interface ILoadableHandler {
  setIsLoading: Dispatch<SetStateAction<boolean>>
  setError: Dispatch<SetStateAction<IApiError | undefined>>
}