import { PencilIcon } from "@heroicons/react/24/solid"
import { useState, useContext } from "react"
import { GroupContext } from "../../contexts"
import { Role } from "../../enums"
import { IGroup, IGroupUser } from "../../models"
import { combineClassNames } from "../../services"
import { Modal } from "../Common"
import UserRoleSelector from "./UserRoleSelector"

interface IGroupUserEntryProps {
  group: IGroup
  gu: IGroupUser
}

const GroupUserEntry = ({ group, gu }: IGroupUserEntryProps) => {

  const [editGroupUsersOpen, setEditGroupUsersOpen] = useState<boolean>(false)
  const toggleEditGroupUsersOpen = () => setEditGroupUsersOpen(!editGroupUsersOpen)

  const { userHasRole } = useContext(GroupContext)

  return (
    <div className="flex">
      <button type="button" onClick={toggleEditGroupUsersOpen} className="w-full grid grid-cols-8 items-center group hover:bg-gray-300 dark:hover:bg-gray-900 rounded-md p-2">
        <div className="col-span-6 flex flex-col text-left">
          <span className="uppercase font-bold text-lg tracking-wider">{gu.username}</span>
          <span className="text-sm tracking-wide dark:text-gray-500 text-gray-500">{gu.email}</span>
        </div>
        <div className="col-span-2 grid grid-cols-6 items-center">
          <div className={combineClassNames(
            "col-span-5 font-bold px-2 py-0.5 border border-current rounded-md",
            gu.hasJoined ? userHasRole(gu.groupId, gu.userId, Role.GroupAdmin) ? "text-orange-500" : "text-blue-500" : "text-emerald-500"
          )}>
            {gu.hasJoined
              ? userHasRole(gu.groupId, gu.userId, Role.GroupAdmin)
                ? 'Admin'
                : 'Member'
              : 'Pending'
            }
          </div>
          <div className="col-span-1 flex flex-1 justify-end">
            <PencilIcon className="w-6 h-6 group-hover:text-blue-500 transition-colors" />
          </div>
        </div>
      </button>
      <Modal
        title="Edit group users"
        description="This dialog can be used to edit and update an existing user's roles within the currently selected group."
        isOpen={editGroupUsersOpen}
        close={toggleEditGroupUsersOpen}
        level={2}
      >
        {(ConfirmationButton) => (
          <UserRoleSelector group={group} groupUser={gu} ContextualSubmissionButton={ConfirmationButton} />
        )}
      </Modal>
    </div>
  )
}

export default GroupUserEntry