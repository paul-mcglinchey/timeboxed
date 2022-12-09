import { Dispatch, SetStateAction } from "react";
import { FormikContextType, useFormikContext } from "formik";
import { ISchedule } from "../../models";
import { SaveAsIcon } from "@heroicons/react/solid";
import { Transition } from "@headlessui/react";
import ScheduleSwitcher from "./ScheduleSwitcher";
import ScheduleTable from "./ScheduleTable";

interface IScheduleProps {
  currentWeek: { first: Date, last: Date, week: Date[] },
  editing: boolean
  currentWeekModifier: number
  setCurrentWeekModifier: Dispatch<SetStateAction<number>>
  scheduleLoading: boolean
}

const Schedule = ({ currentWeek, editing, currentWeekModifier, setCurrentWeekModifier, scheduleLoading }: IScheduleProps) => {

  const { dirty, handleSubmit }: FormikContextType<ISchedule> = useFormikContext()

  const cycleBack = () => updateScheduleWeek(currentWeekModifier - 1);
  const cycleForwards = () => updateScheduleWeek(currentWeekModifier + 1);

  const updateScheduleWeek = (modifier: number) => {
    dirty && handleSubmit();
    setCurrentWeekModifier(modifier);
  }

  return (
    <div className="flex flex-col flex-grow space-y-4 mt-10 text-gray-200">
      <ScheduleSwitcher
        backwards={cycleBack}
        forwards={cycleForwards}
        startDate={currentWeek.first}
        endDate={currentWeek.last}
        modifier={currentWeekModifier}
        scheduleLoading={scheduleLoading}
      />
      <ScheduleTable
        currentWeek={currentWeek}
        editing={editing}
      />
      <Transition
        as="button"
        onClick={() => handleSubmit()}
        type="button"
        show={dirty}
        enter="transform transition duration-[400ms]"
        enterFrom="opacity-0 translate-y-full"
        enterTo="opacity-100 translate-y-0"
        leave="transform duration-200 transition ease-in-out"
        leaveFrom="opacity-100 translate-y-full"
        leaveTo="opacity-0 translate-y-0"
        className="fixed flex items-center justify-center p-4 w-full space-x-4 bottom-0 right-0 bg-blue-500 rounded-t-lg"
      >
        <div className="font-bold tracking-wide text-lg">
          You have unsaved changes
        </div>
        <div>
          <SaveAsIcon className="w-8 h-8" />
        </div>
      </Transition>
    </div>
  )
}

export default Schedule;