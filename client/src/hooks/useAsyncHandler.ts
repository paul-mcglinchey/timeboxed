import { Dispatch, SetStateAction, useCallback } from "react"
import { Notification } from "../enums"
import { IApiError } from "../models/error.model"
import { IAsyncHandler } from "./interfaces"
import useNotification from "./useNotification"

const useAsyncHandler = (setIsLoading: Dispatch<SetStateAction<boolean>>): IAsyncHandler => {

  const { addNotification } = useNotification()

  const asyncHandler = useCallback((fn: (...args: any[]) => any, failureActions: (() => void)[] = []) => async (...args: any) => {
    try {
      setIsLoading(true)
      await fn(...args)
    } catch (err) {
      if (err instanceof Error) {
        console.error(err.message)
        addNotification(err.message.trim().length > 0 ? err.message : 'Something went wrong...', Notification.Error)
      }

      failureActions.forEach(fa => fa())
    } finally {
      setIsLoading(false)
    }
  }, [setIsLoading, addNotification])
  
  const asyncReturnHandler = useCallback(<T>(fn: (...args: any[]) => any) => async (...args: any): Promise<T> => {
    try {
      setIsLoading(true)
      var returnValue = await fn(...args)
    } catch (err) {
      console.error(err as IApiError)
      addNotification((err as Error).message ?? 'Something went wrong...', Notification.Error)
    } finally {
      setIsLoading(false)
      return returnValue
    }
  }, [setIsLoading, addNotification])
  
  return { asyncHandler, asyncReturnHandler }
}

export default useAsyncHandler