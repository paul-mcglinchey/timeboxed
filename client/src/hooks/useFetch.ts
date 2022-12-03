import { Dispatch, SetStateAction, useEffect, useRef, useState } from "react"
import { IFetch } from "../models/fetch.model";
import useAsyncHandler from "./useAsyncHandler";
import useIsMounted from "./useIsMounted";

interface ICache<T> {
  [key: string]: T
}

const useFetch = <T>(
  url: string,
  options: RequestInit,
  deps: any[] = [],
  setIsLoading: Dispatch<SetStateAction<boolean>>,
  setError: Dispatch<SetStateAction<any>>,
  useCache: boolean = false
): IFetch<T> => {
  const [response, setResponse] = useState<T>();
  
  const { asyncHandler } = useAsyncHandler(setIsLoading, setError)

  const cache = useRef<ICache<T>>({})
  const isMounted = useIsMounted()
  
  useEffect(() => {
    const _fetch = asyncHandler(async () => {
      setIsLoading(true)

      if (cache.current[url] && useCache) {
        setResponse(cache.current[url])
      } else {
        const res = await fetch(url, options)
        const json = await res.json()

        if (res.ok) {
          setResponse(json as unknown as T)
          cache.current[url] = json
        } else {
          setError({ message: `${res.status} ${res.statusText}`})
        }

      }

      setIsLoading(false)
    })
    
    isMounted() && _fetch()
    
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, deps);
  
  return { response };
}

export default useFetch;