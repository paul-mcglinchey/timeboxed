import { useEffect, useState } from "react";
import { Formik } from "formik";
import { UserPlusIcon } from "@heroicons/react/24/solid";
import { useNotification, useScheduleService } from "../../hooks";
import { Prompter } from "../Common";
import { IRota, ISchedule, IScheduleShiftRequest } from "../../models";
import { getDateOnly } from "../../services";
import { IApiError } from "../../models/error.model";
import { Notification } from "../../enums";
import RotaHeader from "./RotaHeader";
import Schedule from "./Schedule";

interface ISchedulerResponse {
  rota: IRota
}

const Scheduler = ({ rota }: ISchedulerResponse) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()

  const [editing, setEditing] = useState<boolean>(false)
  const [currentWeekModifier, setCurrentWeekModifier] = useState<number>(0)

  const { getSchedule, updateSchedule, createSchedule, getWeek, getShift } = useScheduleService(setIsLoading, setError)
  const { addNotification } = useNotification()

  const currentWeek = getWeek(currentWeekModifier)
  const startDate = currentWeek.first
  const schedule: ISchedule | undefined = getSchedule(startDate)

  const getBlankShift = (d: Date): IScheduleShiftRequest => ({ date: getDateOnly(d), startHour: null, endHour: null, notes: null })

  useEffect(() => {
    if (error?.message) addNotification(error.message, Notification.Error)
  }, [error, addNotification])

  return (
    <>
      <Formik
        enableReinitialize
        initialValues={schedule
          ? { startDate: schedule.startDate, employeeSchedules: schedule.employeeSchedules.map(es => ({ employeeId: es.employeeId, shifts: currentWeek.week.map(d => getShift(es.shifts, d) ?? getBlankShift(d)) })) }
          : { startDate: getDateOnly(startDate), employeeSchedules: rota.employees.map(re => ({ employeeId: re, shifts: [] })) }
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
                scheduleLoading={isLoading}
              />
            ) : (
              <Prompter
                title="This rota doesn't have any employees assigned"
                action={() => { }}
                Icon={UserPlusIcon}
              />
            )}
          </div>
        )}
      </Formik>
    </>
  )
}

export default Scheduler