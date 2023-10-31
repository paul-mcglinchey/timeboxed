import { CalendarIcon, CheckCircleIcon } from "@heroicons/react/24/outline";
import { useContext, useState } from "react";
import { GroupContext } from "../../contexts/GroupContext";
import { useInterval } from "../../hooks";
import { IActivityLog } from "../../models";
import { parseTimeDifference } from "../../services";

const ActivityLogEntry = ({ al }: { al: IActivityLog }) => {
  const [timeDifference, setTimeDifference] = useState<number | string>('');
  const { currentGroup } = useContext(GroupContext)

  useInterval(() => {
    setTimeDifference(parseTimeDifference(al.updatedAt));
  }, 1000);

  return (
    <div className="flex px-2 items-center justify-between">
      <div className="flex py-2 space-x-2 items-center">
        <CheckCircleIcon className="w-5 text-green-500"/>
        <span className="px-2 py-1 text-sm tracking-wide bg-blue-500/10 rounded-lg">
          {currentGroup?.users.find(gu => gu.userId === al.actor)?.username}
        </span>
        <span className="text-sm text-gray-400">
          {al.task}
        </span>
      </div>
      <span className="flex space-x-2 text-xs items-center">
        <CalendarIcon className="w-3 h-3" />
        <div>
          {timeDifference && timeDifference} ago
        </div>
      </span>
    </div>
  )
}

export default ActivityLogEntry;