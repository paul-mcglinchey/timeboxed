import { createContext, useEffect, useState } from "react";
import { IChildrenProps, IFetch, IClient, IClientsResponse, IFilter } from "../models";
import { useFetch, useGroupService, useRequestBuilder } from "../hooks";
import { clientTableHeaders, endpoints } from "../config";
import { getItemInLocalStorage, setItemInLocalStorage } from "../services";
import { IClientContext } from "./interfaces";
import { SortDirection } from "../enums";

interface IClientProviderProps extends IChildrenProps {
  includeDeleted?: boolean
}

export const ClientContext = createContext<IClientContext>({
  clients: [],
  setClients: () => {},
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
  filters: {},
  setFilters: () => {},
  isLoading: false,
  setIsLoading: () => {},
  error: undefined,
  setError: () => {}
});

const buildQueryString = (pageNumber: number, pageSize: number, sortField: string | undefined, sortDirection: SortDirection, filters: IFilter) => {
  var queryString = "";

  queryString += `pageNumber=${pageNumber}&pageSize=${pageSize}&`;
  queryString += `sortField=${sortField}&sortDirection=${sortDirection}`

  for (var key in filters) {
    if (filters[key]!.value) {
      queryString += `&${key}=${filters[key]!.value}`
    }
  }

  return queryString;
}

export const ClientProvider = ({ children }: IClientProviderProps) => {
  const [clients, setClients] = useState<IClient[]>([])
  const [count, setCount] = useState<number>(0)
  const [isLoading, setIsLoading] = useState<boolean>(true)
  const [error, setError] = useState<any>()

  const [sortField, setSortField] = useState<string | undefined>(clientTableHeaders[0]!.value)
  const [sortDirection, setSortDirection] = useState<SortDirection>(SortDirection.Desc)
  const [pageNumber, setPageNumber] = useState<number>(parseInt(getItemInLocalStorage('clientlist-pagenumber') || "1"));
  const [pageSize, setPageSize] = useState<number>(parseInt(getItemInLocalStorage('clientlist-pagesize') || "10"));

  const [filters, setFilters] = useState<IFilter>({
    'name': { value: null, label: 'Name' }
  });

  const { currentGroup } = useGroupService()
  const { requestBuilder } = useRequestBuilder()
  const { response }: IFetch<IClientsResponse> = useFetch
  (
    `${endpoints.clients(currentGroup?.id || "")}?${buildQueryString(pageNumber, pageSize, sortField, sortDirection, filters)}`, 
    requestBuilder(), 
    [sortField, sortDirection, pageSize, pageNumber, filters, currentGroup],
    setIsLoading,
    setError
  )

  useEffect(() => {
    if (response) {
      setClients(response.items)
      setCount(response.count)
    }
  }, [response])

  useEffect(() => {
    setItemInLocalStorage('clientlist-pagenumber', pageNumber)
  }, [pageNumber])

  useEffect(() => {
    setItemInLocalStorage('clientlist-pagesize', pageSize)
  }, [pageSize])

  const contextValue = {
    clients,
    setClients,
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
    filters,
    setFilters,
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