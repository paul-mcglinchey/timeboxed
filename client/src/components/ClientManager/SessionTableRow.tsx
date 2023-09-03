import { format } from "date-fns"
import { Dispatch, SetStateAction, useState } from "react"
import { useClientService } from "../../hooks"
import { ISession } from "../../models"
import { IApiError } from "../../models/error.model"
import { Dialog, InlineButton, Modal, TableRow, TableRowItem, TagButton } from "../Common"
import UpdateSessionForm from "./UpdateSessionForm"

interface ISessionTableRowProps {
  clientId: string
  session: ISession
  setIsLoading: Dispatch<SetStateAction<boolean>>
  setError: Dispatch<SetStateAction<IApiError | undefined>>
  fetchSessions: (tagIdFilter?: string | undefined) => Promise<void>
}

const SessionTableRow = ({ clientId, session, setIsLoading, setError, fetchSessions }: ISessionTableRowProps) => {

  const [editSessionOpen, setEditSessionOpen] = useState<boolean>(false)
  const [deleteSessionOpen, setDeleteSessionOpen] = useState<boolean>(false)

  const { deleteSession } = useClientService(setIsLoading, setError)

  const handleUpdate = async () => {
    await fetchSessions()
    setEditSessionOpen(false)
  }

  const handleDelete = async () => {
    await deleteSession(clientId, session.id)
    await fetchSessions()
    setDeleteSessionOpen(false)
  }

  return (
    <TableRow>
      <TableRowItem>
        <div className="flex flex-col">
          <span>
            {session.title}
          </span>
          <span className="text-xs dark:text-blue-200 text-blue-700">
            {session.sessionDate ? format(new Date(session.sessionDate), 'do MMMM yyy') : '--'}
          </span>
        </div>
      </TableRowItem>
      <TableRowItem>
        <div className="flex gap-2 flex-wrap">
          {session.tags.map(t => (
            <TagButton tag={t} key={t.id} onClick={() => fetchSessions(t.id)} />
          ))}
        </div>
      </TableRowItem>
      <TableRowItem>
        <div className="flex flex-grow items-center space-x-2 justify-end">
          <InlineButton color="text-blue-500" action={() => setEditSessionOpen(true)}>Edit</InlineButton>
          <InlineButton color="text-rose-500" action={() => setDeleteSessionOpen(true)}>Delete</InlineButton>
        </div>
        <Modal
          title="Edit session"
          description="This dialog can be used to edit existing sessions"
          isOpen={editSessionOpen}
          close={() => setEditSessionOpen(false)}
        >
          {(ConfirmationButton) => (
            <UpdateSessionForm selectedSession={session} ContextualSubmissionButton={ConfirmationButton} submissionAction={handleUpdate} />
          )}
        </Modal>
        <Dialog
          isOpen={deleteSessionOpen}
          close={() => setDeleteSessionOpen(false)}
          positiveAction={handleDelete}
          title="Delete session"
          description="This action will delete the session from the current group and client"
          content="If you choose to continue you'll no longer have access to this session."
        />
      </TableRowItem>
    </TableRow>
  )
}

export default SessionTableRow