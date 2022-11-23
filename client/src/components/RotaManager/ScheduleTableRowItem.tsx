import { useEffect, useMemo, useState } from "react";
import { IEmployeeSchedule, ISchedule } from "../../models"
import { ScheduleShiftInput } from ".";
import { Button, Modal, FormikInput } from "../Common";
import { FormikContextType, getIn, useFormikContext } from "formik";
import { getDateOnly } from "../../services";

interface IScheduleTableRowItemProps {
  employeeSchedule: IEmployeeSchedule,
  values: ISchedule,
  day: number,
  employeeIndex: number,
  index: number,
  editing: boolean
}

const ScheduleTableRowItem = ({ values, day, employeeIndex, index, editing }: IScheduleTableRowItemProps) => {

  const [editShiftOpen, setEditShiftOpen] = useState<boolean>(false)
  const { errors, touched }: FormikContextType<ISchedule> = useFormikContext()

  const currentDate = useMemo(() => new Date(values.startDate), [values.startDate])
  currentDate.setDate(new Date(values.startDate).getDate() + day)

  let cellValues = values?.employeeSchedules[employeeIndex]?.shifts[index]

  useEffect(() => {
    if (cellValues) {
      cellValues.date = getDateOnly(currentDate)
    }
  }, [cellValues, currentDate])

  return (
    <>
      <div className="flex justify-center space-x-1 items-center">
        <button tabIndex={-1} onDoubleClick={editing ? () => setEditShiftOpen(true) : undefined} type="button" className="flex items-center space-x-1 rounded-lg p-2 uppercase border-2 border-transparent focus:border-gray-500">
          {editing ? (
            <>
              <ScheduleShiftInput name={`employeeSchedules.${employeeIndex}.shifts.${index}.startHour`} />
              <div>
                -
              </div>
              <ScheduleShiftInput name={`employeeSchedules.${employeeIndex}.shifts.${index}.endHour`} />
            </>
          ) : (
            <>
              {cellValues?.startHour && cellValues?.endHour ? (
                <div className="flex text-sm md:text-lg font-bold items-center space-x-1">
                  <div>{cellValues.startHour}</div>
                  <div> - </div>
                  <div>{cellValues.endHour}</div>
                </div>
              ) : (
                <div className="text-red-900 tracking-widest font-bold text-sm md:text-lg">
                  OFF
                </div>
              )}
            </>
          )}
        </button>
      </div>
      <Modal
        title="Edit shift"
        description="This dialog can be used to edit a shift"
        isOpen={editShiftOpen}
        close={() => setEditShiftOpen(false)}
      >
        {() => (
          <div className="flex flex-col space-y-4">
            <div className="flex space-x-4">
              <FormikInput
                label="Start hour"
                name={`employeeSchedules.${employeeIndex}.shifts.${index}.startHour`}
                errors={getIn(errors, `employeeSchedules.${employeeIndex}.shifts.${index}.startHour`)}
                touched={getIn(touched, `employeeSchedules.${employeeIndex}.shifts.${index}.startHour`)}
              />
              <FormikInput
                label="Start hour"
                name={`employeeSchedules.${employeeIndex}.shifts.${index}.endHour`}
                errors={getIn(errors, `employeeSchedules.${employeeIndex}.shifts.${index}.endHour`)}
                touched={getIn(touched, `employeeSchedules.${employeeIndex}.shifts.${index}.endHour`)}
              />
            </div>
            <FormikInput
              label="Notes"
              name={`employeeSchedules.${employeeIndex}.shifts.${index}.notes`}
              errors={getIn(errors, `employeeSchedules.${employeeIndex}.shifts.${index}.notes`)}
              touched={getIn(touched, `employeeSchedules.${employeeIndex}.shifts.${index}.notes`)}
            />
            <div className="flex justify-end">
              <Button type="button" content="Edit" action={() => setEditShiftOpen(false)} />
            </div>
          </div>
        )}
      </Modal>
    </>
  )
}

export default ScheduleTableRowItem;