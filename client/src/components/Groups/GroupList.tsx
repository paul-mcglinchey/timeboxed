import { useGroupService } from "../../hooks"
import { IGroup } from "../../models"
import { SpinnerIcon } from "../Common"
import { GroupCard, GroupPrompter } from '.'
import { Dispatch, SetStateAction } from "react"

interface IGroupListProps {
  setAddGroupOpen: Dispatch<SetStateAction<boolean>>
}

const GroupList = ({ setAddGroupOpen }: IGroupListProps) => {

  const { isLoading, groups = [] } = useGroupService()
  
  return (
    <>
      {isLoading ? (
        <div className="flex justify-center py-10">
          <SpinnerIcon className="text-white h-12 w-12" />
        </div>
      ) : (
        groups.length > 0 ? (
          <div className="grid grid-cols-1 xl:grid-cols-2 gap-4">
            {groups.map((g: IGroup, i: number) => (
              <GroupCard key={i} g={g} />
            ))}
          </div>
        ) : (
          <GroupPrompter action={() => setAddGroupOpen(true)} />
        )
      )}
    </>
  )
}

export default GroupList