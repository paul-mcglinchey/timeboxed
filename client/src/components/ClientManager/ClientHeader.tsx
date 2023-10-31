import { ArrowLeftIcon, TrashIcon, SquaresPlusIcon } from "@heroicons/react/24/solid";
import { useEffect, useState } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { Dialog, Dropdown, SpinnerIcon, Modal } from "..";
import { Notification } from "../../enums";
import { useClientService, useNotification } from "../../hooks";
import { IClientListResponse } from "../../models";
import { IApiError } from "../../models/error.model";
import AddSessionForm from "./AddSessionForm";

interface IClientHeaderProps {
  client: IClientListResponse
}

const ClientHeader = ({ client }: IClientHeaderProps) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()

  const { pathname } = useLocation();
  const navigate = useNavigate();
  const { addNotification } = useNotification()

  const [addSessionOpen, setAddSessionOpen] = useState(false);
  const [deleteClientOpen, setDeleteClientOpen] = useState(false);
  const { fetchClients, deleteClient } = useClientService(setIsLoading, setError)

  const getRouteName = () => {
    switch (pathname.split('/').pop()) {
      case "sessions":
        return " / Sessions";
      case "edit":
        return " / Edit";
      case "view":
        return " / View";
      default:
        return "";
    }
  }

  const handleDelete = async () => {
    await deleteClient(client.id)
    setDeleteClientOpen(false)
    navigate('/clients/dashboard')
  }

  const handleAddSession = async () => {
    await fetchClients()
    setAddSessionOpen(false)
  }

  useEffect(() => {
    if (error?.message) addNotification(error.message, Notification.Error)
  }, [error, addNotification])

  return (
    <div className="flex justify-between pb-4">
      <div className="inline-flex items-center space-x-2 dark:text-white text-gray-800 text-2xl font-semibold tracking-wider">
        <Link to="/clients/dashboard" className="flex items-center transform hover:scale-105 transition-all">
          <ArrowLeftIcon className="h-5 w-5" />
          <span className="rounded-lg px-2 py-1">Clients</span>
        </Link>
        <span> / </span>
        <span className="text-green-500">
          <span>{client.firstName} {client.lastName}</span>
        </span>
        <span>{getRouteName()}</span>
        {isLoading && <SpinnerIcon className="w-6 h-6" />}
      </div>
      <div>
        <Dropdown options={[
          { label: 'Add session', action: () => setAddSessionOpen(true), Icon: SquaresPlusIcon },
          { label: 'Delete client', action: () => setDeleteClientOpen(true), Icon: TrashIcon },
        ]} />
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
      <Dialog
        isOpen={deleteClientOpen}
        close={() => setDeleteClientOpen(false)}
        positiveAction={handleDelete}
        title="Delete client"
        description="This action will delete the client from the current group"
        content="If you choose to continue you'll no longer have access to this client - all sessions belonging to the client will also become innaccessible."
      />
    </div>
  )
}

export default ClientHeader;