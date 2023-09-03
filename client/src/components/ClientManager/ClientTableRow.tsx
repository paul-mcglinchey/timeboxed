import { ViewGridAddIcon } from '@heroicons/react/outline';
import { IClientListResponse } from '../../models';
import { InlineLink, InlineButton, TableRow, TableRowItem, Modal, UpdateClientForm, AddSessionForm, Dialog } from '..';
import { useContext, useEffect, useState } from 'react';
import { GroupContext } from '../../contexts';
import { useClientService, useNotification } from '../../hooks';
import { IApiError } from '../../models/error.model';
import { Notification } from '../../enums';
import { PlusIcon } from '@heroicons/react/solid';

const ClientTableRow = ({ client }: { client: IClientListResponse }) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError | undefined>()

  const [editClientOpen, setEditClientOpen] = useState<boolean>(false)
  const [deleteClientOpen, setDeleteClientOpen] = useState<boolean>(false)
  const [addSessionOpen, setAddSessionOpen] = useState<boolean>(false)

  const { fetchClients, deleteClient } = useClientService(setIsLoading, setError)
  const { addNotification } = useNotification()
  const { getGroupUser } = useContext(GroupContext)

  const handleUpdate = async () => {
    await fetchClients()
    setEditClientOpen(false)
  }

  const handleAddSession = async () => {
    await fetchClients()
    setAddSessionOpen(false)
  }

  useEffect(() => {
    if (error?.message) addNotification(error.message, Notification.Error)
  }, [error, addNotification])

  return (
    <TableRow>
      <TableRowItem>
        <div className="flex flex-col gap-0.5">
          <div>{`${client.firstName} ${client.lastName}` || "--"}</div>
          <div className="text-xs dark:text-blue-100 text-blue-800">{client.primaryEmailAddress || "--"}</div>
        </div>
      </TableRowItem>
      <TableRowItem>
        <div className="inline-flex items-center gap-2 justify-start min-w-40">
          <span>
            {new Date(client.updatedAt || "").toLocaleDateString()}
          </span>
          <span className="font-medium px-2 dark:bg-gray-800 bg-blue-100 tracking-wide rounded-lg select-none text-sm">
            {getGroupUser(client.updatedBy)?.username || '--'}
          </span>
        </div>
      </TableRowItem>
      <TableRowItem>
        <div className="flex flex-grow items-center justify-start">
          {client.sessions.length > 0 ? (
            <div className="flex flex-grow justify-between">
              <InlineLink to={`/clients/${client.id}/sessions`} color="dark:text-amber-400 text-amber-600">
                <span className="self-center pt-0.5">Sessions</span>
                <span className="text-lg">{client.sessions.length}</span>
              </InlineLink>
              <InlineButton action={() => setAddSessionOpen(true)} color="dark:text-amber-400 text-amber-600">
                <PlusIcon className="w-5 h-5 text-current" />
              </InlineButton>
            </div>
          ) : (
            <InlineButton action={() => setAddSessionOpen(true)} color="dark:text-green-500 text-green-600">
              <div className="whitespace-nowrap">Add session</div>
              <ViewGridAddIcon className="w-6 h-6" />
            </InlineButton>
          )}
        </div>
        <Modal
          title="Add session"
          description="This dialog can be used to add sessions to existing clients"
          isOpen={addSessionOpen}
          close={() => setAddSessionOpen(false)}
        >
          {(ConfirmationButton) => (
            <AddSessionForm client={client} ContextualSubmissionButton={ConfirmationButton} submissionAction={handleAddSession} />
          )}
        </Modal>
      </TableRowItem>
      <TableRowItem>
        <div className="flex flex-grow items-center space-x-2 justify-end">
          <InlineLink to={`/clients/${client.id}/view`} color="text-gray-500">View</InlineLink>
          <InlineButton action={() => setEditClientOpen(true)} color="text-blue-500">Edit</InlineButton>
          <InlineButton action={() => setDeleteClientOpen(true)} color="text-rose-500">Delete</InlineButton>
        </div>
        <Modal
          title="Update client"
          description="This dialog can be used to update exisiting clients"
          isOpen={editClientOpen}
          close={() => setEditClientOpen(false)}
        >
          {(ConfirmationButton) => (
            <UpdateClientForm ContextualSubmissionButton={ConfirmationButton} clientId={client.id} submissionAction={handleUpdate} />
          )}
        </Modal>
        <Dialog
          isOpen={deleteClientOpen}
          close={() => setDeleteClientOpen(false)}
          positiveAction={() => deleteClient(client.id)}
          title="Delete client"
          description="This action will delete the client from the current group"
          content="If you choose to continue you'll no longer have access to this client - all sessions belonging to the client will also become innaccessible."
          loading={isLoading}
        />
      </TableRowItem>
    </TableRow>
  )
}

export default ClientTableRow;