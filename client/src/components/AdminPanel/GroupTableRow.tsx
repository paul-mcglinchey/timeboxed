import { useContext, useState } from "react"
import { InlineButton, Modal, TableRow, TableRowItem } from ".."
import { IApplication, IGroup } from "../../models"
import { MetaInfoContext } from "../../contexts/MetaInfoContext"
import UpdateGroupForm from "./UpdateGroupForm"
import { PlusIcon } from "@heroicons/react/24/solid"
import { UpdateGroupUsers } from "./UpdateGroupUsers"

interface IGroupTableRowProps {
  group: IGroup
  fetchGroups: () => Promise<void>
}

const GroupTableRow = ({ group, fetchGroups }: IGroupTableRowProps) => {

  const [editGroupOpen, setEditGroupOpen] = useState(false)
  const [editUsersOpen, setEditUsersOpen] = useState(false)

  const { applications } = useContext(MetaInfoContext)

  const getApplication = (applicationId: number): IApplication | null => applications.find(a => a.id === applicationId) ?? null

  return (
    <TableRow>
      <TableRowItem>
        <span className="">{group.name}</span>
      </TableRowItem>
      <TableRowItem>
        <span className="max-w-xs 2xl:max-w-xl overflow-hidden text-ellipsis">{group.description}</span>
      </TableRowItem>
      <TableRowItem>
        <span className="max-w-xs overflow-hidden text-ellipsis">{group.applications.map(a => getApplication(a)?.name).join(", ")}</span>
      </TableRowItem>
      <TableRowItem>
        <div className="inline-flex gap-2 items-center">
          <span>{group.users.length}</span>
          <InlineButton action={() => setEditUsersOpen(true)} color="text-amber-400"><PlusIcon className="w-6 h-6" /></InlineButton>
        </div>
        <Modal
          title="Edit group users"
          description="This modal can be used to add/remove an existing groups users"
          isOpen={editUsersOpen}
          close={() => setEditUsersOpen(false)}
        >
          {(ConfirmationButton) => (
            <UpdateGroupUsers groupId={group.id} ContextualSubmissionButton={ConfirmationButton} />
          )}
        </Modal>
      </TableRowItem>
      <TableRowItem hugRight>
        <InlineButton action={() => setEditGroupOpen(true)} color="text-green-600">Edit</InlineButton>
        <Modal
          title="Edit group"
          description="This modal can be used to edit an existing groups details"
          isOpen={editGroupOpen}
          close={() => setEditGroupOpen(false)}
        >
          {(ConfirmationButton) => (
            <UpdateGroupForm ContextualSubmissionButton={ConfirmationButton} group={group} submissionAction={fetchGroups} />
          )}
        </Modal>
      </TableRowItem>
    </TableRow>
  )
}

export default GroupTableRow