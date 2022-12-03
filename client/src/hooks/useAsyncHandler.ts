import { Dispatch, SetStateAction, useCallback } from "react"
import { IAsyncHandler } from "./interfaces"

const useAsyncHandler = (setIsLoading: Dispatch<SetStateAction<boolean>>, setError: Dispatch<SetStateAction<any>>): IAsyncHandler => {

  const asyncHandler = useCallback((fn: (...args: any[]) => any, failureActions: (() => void)[] = []) => async (...args: any) => {
    try {
      setIsLoading(true)
      await fn(...args)
    } catch (err) {
      console.error(err)
      failureActions.forEach(fa => fa())
      setError(err)
    } finally {
      setIsLoading(false)
    }
  }, [setIsLoading, setError])
  
  const asyncReturnHandler = useCallback(<T>(fn: (...args: any[]) => any) => async (...args: any): Promise<T> => {
    try {
      setIsLoading(true)
      var returnValue = await fn(...args)
    } catch (err) {
      console.error(err)
      setError(err)
    } finally {
      setIsLoading(false)
      return returnValue
    }
  }, [setIsLoading, setError])
  
  return { asyncHandler, asyncReturnHandler }
}

export default useAsyncHandler