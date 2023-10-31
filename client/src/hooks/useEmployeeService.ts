import { Dispatch, SetStateAction, useContext } from "react";
import { IEmployee } from "../models";
import { generateColour } from "../services"
import { EmployeeContext } from "../contexts/EmployeeContext";
import { GroupContext } from "../contexts/GroupContext";
import { useRequestBuilderService, useAsyncHandler, useResolutionService } from '../hooks'
import { endpoints } from '../config'
import { IEmployeeService } from "./interfaces";
import { IApiError } from "../models/error.model";

const useEmployeeService = (setIsLoading: Dispatch<SetStateAction<boolean>>, setError: Dispatch<SetStateAction<IApiError | undefined>>): IEmployeeService => {

  const employeeContext = useContext(EmployeeContext)
  const { setEmployees } = employeeContext
  const { currentGroup } = useContext(GroupContext)
  
  const { buildRequest } = useRequestBuilderService()
  const { asyncHandler } = useAsyncHandler(setIsLoading)
  const { handleResolution } = useResolutionService()

  const addEmployee = asyncHandler(async (values: IEmployee) => {
    if (!currentGroup?.id) throw new Error()

    const res = await fetch(endpoints.employees(currentGroup.id), buildRequest('POST', undefined, { ...values, colour: generateColour() }))

    if (!res.ok && res.status < 500) return setError(await res.json())

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
  
  return { ...employeeContext, addEmployee, updateEmployee, deleteEmployee }
}

export default useEmployeeService