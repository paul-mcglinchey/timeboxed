import { useContext } from "react";
import { IEmployee } from "../models";
import { generateColour } from "../services"
import { EmployeeContext } from "../contexts";
import { useRequestBuilderService, useAsyncHandler, useResolutionService } from '../hooks'
import { endpoints } from '../config'
import useGroupService from "./useGroupService";
import { IEmployeeService } from "./interfaces";

const useEmployeeService = (): IEmployeeService => {

  const employeeContext = useContext(EmployeeContext)
  const { employees, setEmployees, setIsLoading, setError } = employeeContext
  
  const { buildRequest } = useRequestBuilderService()
  const { asyncHandler } = useAsyncHandler(setIsLoading, setError)
  const { handleResolution } = useResolutionService()

  const { currentGroup } = useGroupService()

  const getEmployee = (employeeId: string | undefined): IEmployee | undefined => {
    return employees.find((e) => e.id === employeeId)
  }

  const addEmployee = asyncHandler(async (values: IEmployee) => {
    if (!currentGroup?.id) throw new Error()

    const res = await fetch(endpoints.employees(currentGroup.id), buildRequest('POST', undefined, { ...values, colour: generateColour() }))
    const json = await res.json()

    handleResolution(res, json, 'create', 'employee', [() => addEmployeeInContext(json)])
  })

  const updateEmployee = asyncHandler(async (employeeId: string | undefined, values: IEmployee) => {
    console.log(employeeId, currentGroup)
    if (!employeeId || !currentGroup?.id) throw new Error()

    const res = await fetch(endpoints.employee(employeeId, currentGroup.id), buildRequest('PUT', undefined, { ...values }))
    const json = await res.json()

    handleResolution(res, json, 'update', 'employee', [() => updateEmployeeInContext(employeeId, values)])
  })

  const deleteEmployee = asyncHandler(async (employeeId: string | undefined) => {
    if (!employeeId || !currentGroup?.id) throw new Error()

    const res = await fetch(endpoints.employee(employeeId, currentGroup?.id), buildRequest('DELETE'))
    const json = await res.json()

    handleResolution(res, json, 'delete', 'employee', [() => deleteEmployeeInContext(employeeId)])
  })

  const addEmployeeInContext = (employee: IEmployee) => {
    setEmployees(employees => [...employees, employee])
  }

  const deleteEmployeeInContext = (employeeId: string) => {
    setEmployees(employees => employees.filter(e => e.id !== employeeId))
  }

  const updateEmployeeInContext = (employeeId: string, values: IEmployee) => {
    setEmployees(employees => {
      return employees.map(e => {
        return e.id === employeeId ? { ...e, ...values } : e
      })
    })
  }
  
  return { ...employeeContext, getEmployee, addEmployee, updateEmployee, deleteEmployee }
}

export default useEmployeeService