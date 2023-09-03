import { createContext, useCallback, useContext, useEffect, useState } from "react";
import { IChildrenProps, IClientListResponse, IClientsResponse, IFilter } from "../models";
import { useAsyncHandler, useRequestBuilderService } from "../hooks";
import { clientTableHeaders, endpoints } from "../config";
import { getItemInLocalStorage, setItemInLocalStorage } from "../services";
import { IClientContext } from "./interfaces";
import { SortDirection } from "../enums";
import { GroupContext } from "./GroupContext";

interface IClientProviderProps extends IChildrenProps {
  includeDeleted?: boolean
}

export const ClientContext = createContext<IClientContext>({
  clients: [],
  setClients: () => {},
  fetchClients: () => new Promise<void>(() => {}),
  count: 0,
  setCount: () => {},
  sortField: undefined,
  setSortField: () => {},
  sortDirection: SortDirection.Desc,
  setSortDirection: () => {},
  pageNumber: 1,
  setPageNumber: () => {},
  pageSize: 10,
  setPageSize: () => {},
  filter: { value: null, label: null, name: null },
  setFilter: () => {},
  buildQueryString: () => {},
  isLoading: false,
  error: undefined,
});

export const ClientProvider = ({ children }: IClientProviderProps) => {
  const [clients, setClients] = useState<IClientListResponse[]>([])
  const [count, setCount] = useState<number>(0)
  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<any>()

  const [sortField, setSortField] = useState<string | undefined>(clientTableHeaders[0]!.value)
  const [sortDirection, setSortDirection] = useState<SortDirection>(SortDirection.Desc)
  const [pageNumber, setPageNumber] = useState<number>(parseInt(getItemInLocalStorage('clientlist-pagenumber') || "1"));
  const [pageSize, setPageSize] = useState<number>(parseInt(getItemInLocalStorage('clientlist-pagesize') || "10"));

  const [filter, setFilter] = useState<IFilter>({ value: null, label: null, name: null })

  const { currentGroup } = useContext(GroupContext)
  const { buildRequest } = useRequestBuilderService()
  const { asyncHandler } = useAsyncHandler(setIsLoading)

  const buildQueryString = useCallback(() => {
    var queryString = "?";
  
    queryString += `pageNumber=${pageNumber}&pageSize=${pageSize}&`;
    queryString += `sortField=${sortField}&sortDirection=${sortDirection}`

    if (filter.value) queryString += `${queryString.includes('?') ? '&' : '?'}${filter.name}=${filter.value}`
  
    return queryString;
  }, [filter, pageNumber, pageSize, sortField, sortDirection])

  const fetchClients = useCallback(async () => {
    if (currentGroup) {
      const res = await fetch(`${endpoints.clients(currentGroup.id)}${buildQueryString()}`, buildRequest())
      const json: IClientsResponse = await res.json()

      setClients(json.items)
      setCount(json.count)
    }
  }, [currentGroup, buildQueryString, buildRequest])

  useEffect(() => {

    const _fetch = asyncHandler(async () => await fetchClients())

    _fetch()

  }, [fetchClients, asyncHandler])

  useEffect(() => {
    setItemInLocalStorage('clientlist-pagenumber', pageNumber)
  }, [pageNumber])

  useEffect(() => {
    setItemInLocalStorage('clientlist-pagesize', pageSize)
  }, [pageSize])

  const contextValue = {
    clients,
    setClients,
    fetchClients,
    count,
    setCount,
    sortField,
    setSortField,
    sortDirection,
    setSortDirection,
    pageNumber,
    setPageNumber,
    pageSize,
    setPageSize,
    filter,
    setFilter,
    buildQueryString,
    isLoading,
    setIsLoading,
    error,
    setError
  }

  return (
    <ClientContext.Provider value={contextValue}>
      {children}
    </ClientContext.Provider>
  )
} 