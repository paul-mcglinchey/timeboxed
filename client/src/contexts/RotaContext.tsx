import { createContext, useEffect, useState } from "react";
import { IChildrenProps, IFetch, IRota, IRotasResponse } from "../models";
import { useFetch, useGroupService, useRequestBuilder } from "../hooks";
import { endpoints, rotaTableHeaders } from "../config";
import { IRotaContext } from "./interfaces";
import { SortDirection } from "../enums";

export const RotaContext = createContext<IRotaContext>({
  rotas: [],
  setRotas: () => {},
  count: 0,
  rotaId: undefined,
  setRotaId: () => {},
  sortField: undefined,
  setSortField: () => {},
  sortDirection: SortDirection.Desc,
  setSortDirection: () => {},
  isLoading: false,
  setIsLoading: () => {},
  error: undefined,
  setError: () => {}
});

export const RotaProvider = ({ children }: IChildrenProps) => {
  const [rotaId, setRotaId] = useState<string | undefined>(undefined)
  const [rotas, setRotas] = useState<IRota[]>([])
  const [count, setCount] = useState<number>(0)
  const [isLoading, setIsLoading] = useState<boolean>(true)
  const [error, setError] = useState<any>()

  const [sortField, setSortField] = useState<string | undefined>(rotaTableHeaders[0]!.value)
  const [sortDirection, setSortDirection] = useState<SortDirection>(SortDirection.Desc)

  const { currentGroup } = useGroupService()
  const { requestBuilder } = useRequestBuilder()
  const { response }: IFetch<IRotasResponse> = useFetch(endpoints.rotas(currentGroup?.id || ""), requestBuilder(), [sortField, sortDirection, currentGroup], setIsLoading, setError)

  useEffect(() => {
    if (response) {
      setRotas(response.items)
      setCount(response.count)
    }
  }, [response])

  const contextValue = {
    rotas,
    setRotas,
    count,
    rotaId,
    setRotaId, 
    sortField,
    setSortField,
    sortDirection,
    setSortDirection,
    isLoading,
    setIsLoading,
    error,
    setError
  }

  return (
    <RotaContext.Provider value={contextValue}>
      {children}
    </RotaContext.Provider>
  )
} 