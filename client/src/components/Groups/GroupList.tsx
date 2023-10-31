import { IGroup } from "../../models"
import { SpinnerLoader } from "../Common"
import { Dispatch, SetStateAction, useContext } from "react"
import GroupCard from "./GroupCard"
import GroupPrompter from "./GroupPrompter"
import { GroupContext } from "../../contexts/GroupContext"

interface IGroupListProps {
  setAddGroupOpen: Dispatch<SetStateAction<boolean>>
}

const GroupList = ({ setAddGroupOpen }: IGroupListProps) => {

  const { isLoading, groups } = useContext(GroupContext)

  return (
    <>
      {groups.length > 0 ? (
        <div className="grid grid-cols-1 xl:grid-cols-2 gap-4">
          {groups.map((g: IGroup, i: number) => (
            <GroupCard key={i} g={g} />
          ))}
        </div>
      ) : (
        isLoading ? <SpinnerLoader /> : <GroupPrompter action={() => setAddGroupOpen(true)} />
      )}
    </>
  )
}

export default GroupList