import { Fragment, useContext } from "react";
import { FieldArray } from "formik";
import { IEmployeeSchedule, ISchedule, IScheduleShift } from "../../models";
import { generateColour, getInitials } from "../../services";
import { EmployeeContext } from "../../contexts/EmployeeContext";
import ScheduleTableRowItem from "./ScheduleTableRowItem";

interface IScheduleTableRowProps {
  employeeSchedule: IEmployeeSchedule
  dayCycle: number[]
  employeeIndex: number
  values: ISchedule
  currentWeek: { first: Date, last: Date, week: Date[] }
  editing: boolean
}

const ScheduleTableRow = ({ employeeSchedule, dayCycle, employeeIndex, values, currentWeek, editing }: IScheduleTableRowProps) => {

  const { getEmployee } = useContext(EmployeeContext)
  const employee = getEmployee(employeeSchedule.employeeId)

  const getShift = (date: Date | undefined): IScheduleShift | undefined => {
    return values.employeeSchedules[employeeIndex]?.shifts.find(s => date && s.date === date.toISOString().split("T")[0])
  }

  const getHours = (shift: IScheduleShift | undefined) => {

    if (!shift) return 0

    let startHour = shift.startHour
    let endHour = shift.endHour

    if (!startHour || !endHour || startHour.length === 0 || endHour.length === 0) {
      return 0;
    }

    let startValue: number = Number(startHour);
    let endValue = endHour;

    let start = Number(startValue);
    let end: number = 0;

    if (endValue?.toLowerCase().includes('f') || endValue?.toLowerCase().includes('c')) {
      end = 10;
    } else {
      end = Number(endValue);
    }

    let result = (end < 12 ? end + 12 : end) - (start < 8 ? start + 12 : start);
    return Number.isNaN(result) ? 0 : result;
  }

  const getTotalHours = () => {

    let shifts = values.employeeSchedules[employeeIndex]?.shifts;
    let total: number = 0;

    shifts && shifts.forEach((s: IScheduleShift) => {
      if (!s || !s.startHour || !s.endHour) return;

      total += getHours(s);
    })

    return total;
  }

  return (
    <tr className="bg-gray-900 border-gray-700">
      <th className="sticky left-0 bg-gray-900">
        <div className="md:flex flex-col space-y-1 py-6 px-6 text-left tracking-wider hidden">
          <div>
            {employee?.firstName || '--'} {employee?.lastName}
          </div>
          <div className="text-sm font-light">
            {employee?.primaryEmailAddress || '--'}
          </div>
        </div>
        <div className="block md:hidden p-1 pr-6">
          <div style={{ backgroundColor: employee?.colour || generateColour() }} className="p-1 rounded-full text-gray-900 font-extrabold tracking-wide">
            {getInitials(`${employee?.firstName} ${employee?.lastName}`)}
          </div>
        </div>
      </th>
      <FieldArray
        name="employeeSchedules"
        render={() => (
          <>
            {dayCycle.map((day: number, index: number) => (
              <Fragment key={index}>
                <td className="px-4 py-6 md:py-4 text-sm whitespace-nowrap text-gray-400">
                  <ScheduleTableRowItem employeeIndex={employeeIndex} index={index} employeeSchedule={employeeSchedule} values={values} day={day} editing={editing} />
                </td>
                <td className="hidden md:table-cell">
                  <div className="p-1 border border-gray-800 rounded text-center text-xs w-6 md:text-lg md:w-8 font-semibold tracking-wide text-gray-400">
                    {getHours(getShift(currentWeek.week[day]))}
                  </div>
                </td>
              </Fragment>
            ))}
          </>
        )}
      />
      <td className="px-6 text-center">
        {getTotalHours()}
      </td>
    </tr>
  )
}

export default ScheduleTableRow;