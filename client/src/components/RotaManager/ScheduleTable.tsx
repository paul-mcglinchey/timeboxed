import { Fragment } from "react";
import { Form, FormikContextType, useFormikContext } from "formik";
import { LockClosedIcon } from "@heroicons/react/solid";
import { IEmployeeSchedule, ISchedule } from "../../models";
import { Button } from "../Common";
import { ScheduleTableRow } from '.'
import { getInitials } from "../../services";
import { DayOfWeek } from "../../enums";
import { addDays } from "date-fns/esm";

interface IScheduleTableProps {
  currentWeek: { first: Date, last: Date, week: Date[] }
  editing: boolean
}

const ScheduleTable = ({ currentWeek, editing }: IScheduleTableProps) => {

  const { values, dirty }: FormikContextType<ISchedule> = useFormikContext()
  const dayCycle: number[] = [0, 1, 2, 3, 4, 5, 6];

  return (
    <div className="overflow-x-scroll md:overflow-x-auto rounded-md">
      <Form>
        <table className="min-w-full">
          <thead className="bg-gray-800">
            <tr>
              <th className="px-6 sticky left-0 bg-gray-800">
                <div className="hidden md:block">
                  {values.locked ? (
                    <div>
                      <LockClosedIcon className="text-red-500 w-5 h-5" />
                    </div>
                  ) : (
                    editing && dirty && (
                      <Button buttonType="Tertiary" content='Save' type="submit" />
                    )
                  )}
                </div>
              </th>
              {dayCycle.map((day: number, index: number) => (
                <Fragment key={index}>
                  <th
                    scope="col"
                    className="px-3 md:px-6 text-xs font-medium tracking-wider text-gray-400 uppercase"
                    key={day}
                  >
                    <div className="flex justify-center space-x-2 items-center">
                      <span className="inline sm:hidden">
                        {getInitials(DayOfWeek[day])}
                      </span>
                      <span className="hidden sm:inline">
                        {DayOfWeek[day]}
                      </span>
                      <span className="text-white text-sm">
                        {addDays(currentWeek.first, day).getDate()}
                      </span>
                    </div>
                  </th>
                  <th className="hidden sm:table-cell"></th>
                </Fragment>
              ))}
              <th
                scope="col"
                className="py-3 px-6 text-xs font-medium tracking-wider text-center text-gray-400 uppercase"
              >
                Total hours
              </th>
            </tr>
          </thead>
          <tbody className="divide-y divide-gray-700">
            {values.employeeSchedules.map((employeeSchedule: IEmployeeSchedule, index: number) => (
              <ScheduleTableRow key={index} employeeIndex={index} employeeSchedule={employeeSchedule} values={values} dayCycle={dayCycle} currentWeek={currentWeek} editing={editing} />
            ))}
          </tbody>
        </table>
      </Form>
    </div>
  )
}

export default ScheduleTable;