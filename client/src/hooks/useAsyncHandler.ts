import { Dispatch, SetStateAction, useCallback } from "react"
import { useErrorHandler } from "react-error-boundary"
import { IApiError } from "../models/error.model"
import { IAsyncHandler } from "./interfaces"

const useAsyncHandler = (setIsLoading: Dispatch<SetStateAction<boolean>>): IAsyncHandler => {

  const handleError = useErrorHandler()

  const asyncHandler = useCallback((fn: (...args: any[]) => any, failureActions: (() => void)[] = []) => async (...args: any) => {
    try {
      setIsLoading(true)
      await fn(...args)
    } catch (err) {
      console.error(err as IApiError)
      failureActions.forEach(fa => fa())
      handleError(err)
    } finally {
      setIsLoading(false)
    }
  }, [setIsLoading, handleError])
  
  const asyncReturnHandler = useCallback(<T>(fn: (...args: any[]) => any) => async (...args: any): Promise<T> => {
    try {
      setIsLoading(true)
      var returnValue = await fn(...args)
    } catch (err) {
      console.error(err as IApiError)
      handleError(err)
    } finally {
      setIsLoading(false)
      return returnValue
    }
  }, [setIsLoading, handleError])
  
  return { asyncHandler, asyncReturnHandler }
}

export default useAsyncHandler