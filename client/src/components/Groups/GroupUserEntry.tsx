import { PencilIcon } from "@heroicons/react/24/solid"
import { useState, useContext } from "react"
import { GroupContext } from "../../contexts"
import { Role } from "../../enums"
import { IGroup, IGroupUser } from "../../models"
import { combineClassNames } from "../../services"
import { InlineButton, Modal, TableRow, TableRowItem } from "../Common"
import UserRoleSelector from "./UserRoleSelector"

interface IGroupUserEntryProps {
  group: IGroup
  gu: IGroupUser
}

const GroupUserEntry = ({ group, gu }: IGroupUserEntryProps) => {

  const [editGroupUsersOpen, setEditGroupUsersOpen] = useState<boolean>(false)

  const { userHasRole } = useContext(GroupContext)

  return (
    <TableRow action={() => setEditGroupUsersOpen(true)}>
      <TableRowItem stack>
        <span className="uppercase font-bold text-lg tracking-wider">{gu.username}</span>
        <span className="text-sm tracking-wide dark:text-gray-500 text-gray-500">{gu.email}</span>
      </TableRowItem>
      <TableRowItem>
        <div className={combineClassNames(
          "font-bold px-2 py-0.5 border border-current rounded-md",
          gu.hasJoined
            ? userHasRole(gu.groupId, gu.userId, Role.GroupAdmin)
              ? "text-orange-500"
              : "text-blue-500"
            : "text-emerald-500"
        )}>
          {gu.hasJoined
            ? userHasRole(gu.groupId, gu.userId, Role.GroupAdmin)
              ? 'Admin'
              : 'Member'
            : 'Pending'
          }
        </div>
      </TableRowItem>
      <TableRowItem hugRight>
        <InlineButton action={() => setEditGroupUsersOpen(true)} color="text-amber-500">
          <PencilIcon className="w-6 h-6 hover:text-blue-500 transition-colors" />
        </InlineButton>
        <Modal
          title="Edit group users"
          description="This dialog can be used to edit and update an existing user's roles within the currently selected group."
          isOpen={editGroupUsersOpen}
          close={() => setEditGroupUsersOpen(false)}
          level={2}
        >
          {(ConfirmationButton) => (
            <UserRoleSelector group={group} groupUser={gu} ContextualSubmissionButton={ConfirmationButton} />
          )}
        </Modal>
      </TableRowItem>
    </TableRow>
  )
}

export default GroupUserEntry