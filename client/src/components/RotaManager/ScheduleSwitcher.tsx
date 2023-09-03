import { ChevronLeftIcon, ChevronRightIcon } from "@heroicons/react/24/solid";
import { SpinnerIcon, SquareIconButton } from "../Common";

interface IScheduleSwitcherProps {
  backwards: () => void
  forwards: () => void
  startDate: Date
  endDate: Date
  modifier: number
  scheduleLoading: boolean
}

const ScheduleSwitcher = ({ backwards, forwards, startDate, endDate, modifier, scheduleLoading }: IScheduleSwitcherProps) => {
  return (
    <div className="flex justify-between items-center text-xl font-bold tracking-wide">
      <div className="flex items-center space-x-2">
        <SquareIconButton Icon={ChevronLeftIcon} action={() => backwards()} />
        <div className="hidden md:block">
          {startDate.toLocaleDateString()}
        </div>
      </div>
      <div>
        {modifier === 0 ? 'Current week' : (
          modifier > 0
            ? modifier === 1 ? 'Next week' : modifier + ' weeks away'
            : modifier === -1 ? 'Last week' : Math.abs(modifier) + ' weeks ago'
        )}
      </div>
      {scheduleLoading && <SpinnerIcon className="w-6 h-6"/>}
      <div className="flex items-center space-x-2">
        <div className="hidden md:block">
          {endDate.toLocaleDateString()}
        </div>
        <SquareIconButton Icon={ChevronRightIcon} action={() => forwards()} />
      </div>
    </div>
  )
}

export default ScheduleSwitcher;