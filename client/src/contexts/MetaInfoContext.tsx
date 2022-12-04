import { createContext, useContext, useEffect, useState } from "react";
import { IChildrenProps, IFetch, IMetaInfo } from "../models";
import { useFetch, useRequestBuilderService } from "../hooks";
import { endpoints } from "../config";
import { IMetaInfoContext } from "./interfaces";
import { GroupContext } from "./GroupContext";

export const MetaInfoContext = createContext<IMetaInfoContext>({
  applications: [],
  roles: [],
  permissions: [],
  isLoading: false,
  setIsLoading: () => {},
  error: null,
  setError: () => {}
});

export const MetaInfoProvider = ({ children }: IChildrenProps) => {
  const [metaInfo, setMetaInfo] = useState<IMetaInfo | undefined>()
  const [isLoading, setIsLoading] = useState<boolean>(true)
  const [error, setError] = useState<any>()

  const { buildRequest } = useRequestBuilderService()
  const { currentGroup } = useContext(GroupContext)
  const { response }: IFetch<IMetaInfo> = useFetch(endpoints.metainfo(currentGroup?.id), buildRequest(), [currentGroup], setIsLoading, setError)

  useEffect(() => {
    if (response) {
      setMetaInfo(response)
    }
  }, [response])

  const contextValue = {
    ...metaInfo,
    isLoading,
    setIsLoading,
    error,
    setError
  }

  return (
    <MetaInfoContext.Provider value={contextValue}>
      {children}
    </MetaInfoContext.Provider>
  )
} 