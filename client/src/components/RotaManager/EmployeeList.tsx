import { Dispatch, SetStateAction, useEffect } from "react";
import { UserIcon } from "@heroicons/react/solid";
import { useEmployeeService } from "../../hooks";
import { IEmployee } from "../../models";
import { Prompter, Table } from "../Common";
import { EmployeeTableRow } from ".";

const headers = [
  { name: "Employee name", value: "name", interactive: true },
  { name: "Options", value: "", interactive: false },
]

interface IEmployeeListProps {
  setAddEmployeesOpen: Dispatch<SetStateAction<boolean>>
}

const EmployeeList = ({ setAddEmployeesOpen }: IEmployeeListProps) => {

  const { employees, isLoading, sortField, setSortField, sortDirection, setSortDirection } = useEmployeeService()

  useEffect(() => {
    setSortField(headers[1]!.value)
  }, [setSortField])

  return (
    <>
      <div className="rounded-lg flex flex-col space-y-0 pb-2 min-h-96">
        {employees.length > 0 ? (
          <div className="flex flex-col flex-grow space-y-4">
            <Table isLoading={isLoading}>
              <Table.SortableHeader headers={headers} sortField={sortField} setSortField={setSortField} sortDirection={sortDirection} setSortDirection={setSortDirection} />
              <Table.Body>
                {employees.map((employee: IEmployee, index: number) => (
                  <EmployeeTableRow employee={employee} key={index} />
                ))}
              </Table.Body>
            </Table>
          </div>
        ) : (
          <Prompter
            title="Add your employees here"
            Icon={UserIcon}
            action={() => setAddEmployeesOpen(true)}
          />
        )}
      </div>
    </>
  )
}

export default EmployeeList;