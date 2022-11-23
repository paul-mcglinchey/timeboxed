import { Dispatch, SetStateAction, useCallback } from "react"
import { useNotification } from "."
import { Notification } from "../enums"
import { IAsyncHandler } from "./interfaces"

const useAsyncHandler = (setIsLoading: Dispatch<SetStateAction<boolean>>): IAsyncHandler => {
  const { addNotification } = useNotification()

  const asyncHandler = useCallback((fn: (...args: any[]) => any, failureActions: (() => void)[] = [], notify: boolean = true) => async (...args: any) => {
    try {
      setIsLoading(true)
      await fn(...args)
    } catch (err) {
      console.error(err)
      failureActions.forEach(fa => fa())
      notify && addNotification(err instanceof Error ? err.message : "Something went wrong...", Notification.Error)
    } finally {
      setIsLoading(false)
    }
  }, [addNotification, setIsLoading])
  
  const asyncReturnHandler = useCallback(<T>(fn: (...args: any[]) => any) => async (...args: any): Promise<T> => {
    try {
      setIsLoading(true)
      var returnValue = await fn(...args)
    } catch (err) {
      console.error(err)
      addNotification('Something went wrong...', Notification.Error)
    } finally {
      setIsLoading(false)

      return returnValue
    }
  }, [addNotification, setIsLoading])
  
  return { asyncHandler, asyncReturnHandler }
}

export default useAsyncHandler