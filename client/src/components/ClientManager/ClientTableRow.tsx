import { ViewGridAddIcon } from '@heroicons/react/outline';
import { useGroupUserService } from '../../hooks';
import { IClientListResponse } from '../../models';
import { InlineLink, InlineButton, TableRow, TableRowItem, Modal, UpdateClientForm, AddSessionForm } from '..';
import { useState } from 'react';

const ClientTableRow = ({ client }: { client: IClientListResponse }) => {

  const [editClientOpen, setEditClientOpen] = useState<boolean>(false)
  const [addSessionOpen, setAddSessionOpen] = useState<boolean>(false)

  const { getGroupUser } = useGroupUserService()

  return (
    <TableRow>
      <TableRowItem>
        <div className="flex flex-col">
          <div className="text-sm font-medium text-white">{`${client.firstName} ${client.lastName}` || "--"}</div>
          <div className="text-sm">{client.primaryEmailAddress || "--"}</div>
        </div>
      </TableRowItem>
      <TableRowItem>
        <div className="flex items-center space-x-4 min-w-40">
          <span>
            {new Date(client.updatedAt || "").toLocaleDateString()}
          </span>
          <span className="font-medium px-2 bg-gray-800 tracking-wide rounded-lg select-none">
            <div>{getGroupUser(client.updatedBy)?.username || '--'}</div>
          </span>
        </div>
      </TableRowItem>
      <TableRowItem>
        <div className="flex flex-grow items-center justify-start">
          {client.sessions.length > 0 ? (
            <InlineLink to={`/clients/${client.id}/addsession`} color="text-amber-400">
              <span className="self-center pt-0.5">Sessions</span>
              <span className="text-lg">{client.sessions.length}</span>
            </InlineLink>
          ) : (
            <InlineButton action={() => setAddSessionOpen(true)} color="text-green-500">
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
            <AddSessionForm client={client} ContextualSubmissionButton={ConfirmationButton}/>
          )}
        </Modal>
      </TableRowItem>
      <TableRowItem>
        <div className="flex flex-grow items-center space-x-2 justify-end">
          <InlineLink to={`/clients/${client.id}/view`} color="text-gray-500">View</InlineLink>
          <InlineButton action={() => setEditClientOpen(true)} color="text-blue-500">Edit</InlineButton>
        </div>
        <Modal
          title="Update client"
          description="This dialog can be used to update exisiting clients"
          isOpen={editClientOpen}
          close={() => setEditClientOpen(false)}
        >
          {(ConfirmationButton) => (
            <UpdateClientForm ContextualSubmissionButton={ConfirmationButton} clientId={client.id} />
          )}
        </Modal>
      </TableRowItem>
    </TableRow>
  )
}

export default ClientTableRow;