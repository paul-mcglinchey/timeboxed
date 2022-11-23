import { useState } from "react";
import { useEmployeeService } from "../../hooks";
import { IEmployee, IFilter } from "../../models";
import { combineClassNames } from "../../services";
import { MultiSelector, SearchBar } from "../Common";

interface IEmployeeMultiSelectorProps {
  formValues: string[],
  setFieldValue: (value: string[]) => void
  fieldName?: string,
}

interface IEmployeeSelectorProps {
  e: IEmployee | undefined
}

const EmployeeSelector = ({ e }: IEmployeeSelectorProps) => {
  return e ? (
    <div className="flex flex-col text-left">
      <span className="font-bold tracking-wider text-lg uppercase">{e.firstName} {e.lastName}</span>
      <span className="text-sm italic">{e.primaryEmailAddress}</span>
    </div>
  ) : <></>
}

const EmployeeMultiSelector = ({ formValues, setFieldValue, fieldName = 'employees' }: IEmployeeMultiSelectorProps) => {

  const [employeeFilter, setEmployeeFilter] = useState<IFilter>({ value: null, name: 'name', label: 'Name' })
  const [showAll, setShowAll] = useState<boolean>(false)
  const toggleShowAll = () => setShowAll(!showAll)

  const { employees, getEmployee } = useEmployeeService()

  return (
    <div className="flex flex-col space-y-2">
      <SearchBar setFilter={setEmployeeFilter} backgroundColorClasses="bg-gray-400 dark:bg-gray-700" />
      <MultiSelector<string>
        fieldName={fieldName}
        values={employees.filter(e => 
          e && employeeFilter.value
            ? e.firstName.concat(" ", e.lastName).toLowerCase().includes(employeeFilter.value.toLowerCase()) 
            : true
          )
          .slice(0, showAll ? employees.length : 5)
          .map(e => e.id)
          .filter((e): e is string => !!e)
        }
        totalValuesLength={employees.length}
        toggleShowAll={toggleShowAll}
        formValues={formValues}
        setFieldValue={(e) => setFieldValue(e)}
        itemStyles={(selected) => combineClassNames(selected ? 'bg-green-400 text-gray-800' : 'text-gray-300', 'flex justify-between p-2 rounded items-center border-b border-gray-200/20 transition-all')}
        render={(e) => (
          <div>
            <EmployeeSelector e={getEmployee(e)} />
          </div>
        )}
      />
    </div>
  )
}

export default EmployeeMultiSelector;