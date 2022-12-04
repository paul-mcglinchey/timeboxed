import { createContext, useContext, useEffect, useState } from "react";
import { IChildrenProps, IFetch, IEmployee, IEmployeesResponse } from "../models";
import { useFetch, useRequestBuilderService } from "../hooks";
import { endpoints } from "../config";
import { IEmployeeContext } from "./interfaces";
import { SortDirection } from "../enums";
import { GroupContext } from "./GroupContext";

export const EmployeeContext = createContext<IEmployeeContext>({
  employees: [],
  setEmployees: () => {},
  count: 0,
  setCount: () => {},
  sortField: undefined,
  setSortField: () => {},
  sortDirection: SortDirection.Desc,
  setSortDirection: () => {},
  isLoading: false,
  setIsLoading: () => {},
  error: undefined,
  setError: () => {}
});

export const EmployeeProvider = ({ children }: IChildrenProps) => {
  const [employees, setEmployees] = useState<IEmployee[]>([])
  const [count, setCount] = useState<number>(0)
  const [isLoading, setIsLoading] = useState<boolean>(true)
  const [error, setError] = useState<any>()

  const [sortField, setSortField] = useState<string | undefined>(undefined)
  const [sortDirection, setSortDirection] = useState<SortDirection>(SortDirection.Desc)

  const { currentGroup } = useContext(GroupContext)
  const { buildRequest } = useRequestBuilderService()
  const { response }: IFetch<IEmployeesResponse> = useFetch(endpoints.employees(currentGroup?.id || ""), buildRequest(), [sortField, sortDirection, currentGroup], setIsLoading, setError)

  useEffect(() => {
    if (response) {
      setEmployees(response.items)
      setCount(response.count)
    }
  }, [response])

  const contextValue = {
    employees,
    setEmployees,
    count,
    setCount,
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
    <EmployeeContext.Provider value={contextValue}>
      {children}
    </EmployeeContext.Provider>
  )
} 