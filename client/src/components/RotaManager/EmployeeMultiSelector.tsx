import { SearchIcon } from "@heroicons/react/solid";
import { useState } from "react";
import { useEmployeeService } from "../../hooks";
import { IEmployee } from "../../models";
import { combineClassNames } from "../../services";
import { MultiSelector, SquareIcon } from "../Common";

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
    <div className="flex flex-col text-left space-y-2 leading-loose">
      <div className="font-bold tracking-wider text-lg uppercase">{e.firstName} {e.lastName}</div>
      <div className="text-sm">{e.primaryEmailAddress}</div>
    </div>
  ) : <></>
}

const EmployeeMultiSelector = ({ formValues, setFieldValue, fieldName = 'employees' }: IEmployeeMultiSelectorProps) => {

  const [employeeFilter, setEmployeeFilter] = useState<string>()
  const [showAll, setShowAll] = useState<boolean>(false)
  const toggleShowAll = () => setShowAll(!showAll)

  const { employees, getEmployee } = useEmployeeService()

  return (
    <div className="flex flex-col space-y-2">
      <div className="flex items-center bg-gray-900 rounded focus-within:outline outline-1 outline-green-500 text-gray-300 h-10 pr-4 space-x-4">
        <input id="filter" autoComplete="off" className="w-full py-2 px-4 bg-gray-900 focus:outline-0 rounded caret-green-500" placeholder="Filter employees..." value={employeeFilter} onChange={(e) => setEmployeeFilter(e.target.value)} />
        <span className="hidden md:block p-0.5 px-1 text-sm text-gray-200 bg-blue-500 uppercase tracking-widest font-bold whitespace-pre rounded">CTRL + K</span>
        <SquareIcon Icon={SearchIcon} />
      </div>
      <MultiSelector<string>
        fieldName={fieldName}
        values={employees.filter(e => 
          e && employeeFilter
            ? e.firstName.concat(" ", e.lastName).toLowerCase().includes(employeeFilter.toLowerCase()) 
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
        itemStyles={(selected) => combineClassNames(selected ? 'bg-green-400 text-gray-800' : 'text-gray-300 bg-gray-900', 'flex p-4 rounded transition-all')}
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