import { Dispatch, SetStateAction } from "react"
import { IApiError } from "./error.model"

export interface ILoadable {
  isLoading: boolean
  setIsLoading: Dispatch<SetStateAction<boolean>>
  error: IApiError | undefined
  setError: Dispatch<SetStateAction<IApiError | undefined>>
}