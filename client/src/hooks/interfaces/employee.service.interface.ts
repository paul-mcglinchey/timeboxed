import { IEmployeeContext } from "../../contexts/interfaces"
import { IAddEmployeeRequest, IEmployee, IUpdateEmployeeRequest } from "../../models"

export interface IEmployeeService extends IEmployeeContext {
  getEmployee: (employeeId: string) => IEmployee | undefined
  addEmployee: (values: IAddEmployeeRequest) => void
  updateEmployee: (employeeId: string, values: IUpdateEmployeeRequest) => void
  deleteEmployee: (employeeId: string) => void
}