import { Dispatch, SetStateAction } from "react"

export interface ILoadable {
  isLoading: boolean
  setIsLoading: Dispatch<SetStateAction<boolean>>
  error: any
  setError: Dispatch<SetStateAction<any>>
}