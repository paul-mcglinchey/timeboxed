import { UsersIcon } from "@heroicons/react/24/outline";

interface IGroupInfoDisplayProps {
  groupCount: number
}

const GroupInfoDisplay = ({ groupCount }: IGroupInfoDisplayProps) => {

  return (
    <div className="hidden md:flex items-center text-gray-400 space-x-1">
      <UsersIcon className="h-4 w-4" />
      <div>
        <span className="font-light align-baseline">
          <span className="font-medium">{groupCount}</span> group{groupCount > 1 ? 's' : ''}
        </span>
      </div>
    </div>
  )
}

export default GroupInfoDisplay;