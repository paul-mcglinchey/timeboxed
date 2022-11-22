import { useState } from "react";
import { Formik } from "formik";
import { UserAddIcon } from "@heroicons/react/solid";
import { useRotaService, useScheduleService } from "../../hooks";
import { Prompter, SpinnerLoader } from "../Common";
import { RotaHeader, Schedule } from "."
import { IRota, ISchedule, IScheduleShiftRequest } from "../../models";
import { getDateOnly } from "../../services";

interface ISchedulerResponse {
  rota: IRota
}

const Scheduler = ({ rota }: ISchedulerResponse) => {
  const [editing, setEditing] = useState<boolean>(false)
  const [currentWeekModifier, setCurrentWeekModifier] = useState<number>(0)

  const { isLoading: isRotaLoading, } = useRotaService()
  const { getSchedule, updateSchedule, createSchedule, getWeek, isLoading: isScheduleLoading, getShift } = useScheduleService()

  const currentWeek = getWeek(currentWeekModifier)
  const startDate = currentWeek.first

  const schedule: ISchedule | undefined = getSchedule(startDate, rota, currentWeek.week)

  const getBlankShift = (d: Date): IScheduleShiftRequest => ({ date: getDateOnly(d), startHour: null, endHour: null, notes: null })

  return (
    <>
      {!isScheduleLoading ? (
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
        (isRotaLoading || isScheduleLoading) && (
          <SpinnerLoader />
        )
      )}
    </>
  )
}

export default Scheduler