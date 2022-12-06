import { useContext, useState } from "react";
import { Formik } from "formik";
import { UserAddIcon } from "@heroicons/react/solid";
import { useScheduleService } from "../../hooks";
import { Prompter, SpinnerLoader } from "../Common";
import { RotaHeader, Schedule } from "."
import { IRota, ISchedule, IScheduleShiftRequest } from "../../models";
import { getDateOnly } from "../../services";
import { RotaContext } from "../../contexts";
import { IApiError } from "../../models/error.model";

interface ISchedulerResponse {
  rota: IRota
}

const Scheduler = ({ rota }: ISchedulerResponse) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()

  const [editing, setEditing] = useState<boolean>(false)
  const [currentWeekModifier, setCurrentWeekModifier] = useState<number>(0)

  const { isLoading: isRotaLoading } = useContext(RotaContext)
  const { getSchedule, updateSchedule, createSchedule, getWeek, getShift } = useScheduleService(setIsLoading, setError)

  const currentWeek = getWeek(currentWeekModifier)
  const startDate = currentWeek.first

  const schedule: ISchedule | undefined = getSchedule(startDate, rota, currentWeek.week)

  const getBlankShift = (d: Date): IScheduleShiftRequest => ({ date: getDateOnly(d), startHour: null, endHour: null, notes: null })

  return (
    <>
      {schedule ? (
        <Formik
          enableReinitialize
          initialValues={schedule
            ? { startDate: schedule.startDate, employeeSchedules: schedule.employeeSchedules.map(es => ({ employeeId: es.employeeId, shifts: currentWeek.week.map(d => getShift(es.shifts, d) ?? getBlankShift(d))}))} 
            : { startDate: getDateOnly(startDate), employeeSchedules: rota.employees.map(re => ({ employeeId: re, shifts: [] }))}
          }
          onSubmit={(values) => {
            schedule ? updateSchedule(values, schedule.id, rota.id) : createSchedule(values, rota.id)
          }}
        >
          {() => (
            <div className="flex flex-col">
              <RotaHeader
                rota={rota}
                editing={editing}
                setEditing={setEditing}
              />
              {(rota.employees.length || 0) > 0 ? (
                <Schedule
                  editing={editing}
                  currentWeek={currentWeek}
                  currentWeekModifier={currentWeekModifier}
                  setCurrentWeekModifier={setCurrentWeekModifier}
                />
              ) : (
                <Prompter
                  title="This rota doesn't have any employees assigned"
                  action={() => { }}
                  Icon={UserAddIcon}
                />
              )}
            </div>
          )}
        </Formik>
      ) : (
        (isRotaLoading || isLoading) ? (
          <SpinnerLoader />
        ) : (
          error ? Error : null
        )
      )}
    </>
  )
}

export default Scheduler