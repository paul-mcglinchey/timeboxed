import { ArrowLeftIcon, TrashIcon } from "@heroicons/react/solid";
import { useEffect, useState } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { SquareIconButton, Dialog, Dropdown, SpinnerIcon } from "..";
import { Notification } from "../../enums";
import { useClientService, useNotification } from "../../hooks";
import { IClient } from "../../models";
import { IApiError } from "../../models/error.model";

interface IClientHeaderProps {
  client: IClient
}

const ClientHeader = ({ client }: IClientHeaderProps) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()

  const { pathname } = useLocation();
  const navigate = useNavigate();
  const { addNotification } = useNotification()

  const [deleteClientOpen, setDeleteClientOpen] = useState(false);
  const { deleteClient } = useClientService(setIsLoading, setError)

  const getRouteName = () => {
    switch (pathname.split('/').pop()) {
      case "addsession":
        return " / Add session";
      case "edit":
        return " / Edit";
      case "view":
        return " / View";
      default:
        return "";
    }
  }

  useEffect(() => {
    if (error?.message) addNotification(error.message, Notification.Error)
  }, [error])

  return (
    <div className="flex justify-between">
      <div className="inline-flex items-center space-x-2 text-white text-2xl font-semibold tracking-wider">
        <Link to="/clients/dashboard">
          <SquareIconButton Icon={ArrowLeftIcon} className="h-5 w-5 transform hover:scale-105 transition-all" />
        </Link>
        <Link to="/clients/dashboard">
          <span className="rounded-lg bg-gray-800 px-2 py-1">Clients</span>
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
          { label: 'Delete', action: () => setDeleteClientOpen(true), Icon: TrashIcon },
        ]} />
      </div>
      <Dialog
        isOpen={deleteClientOpen}
        close={() => setDeleteClientOpen(false)}
        positiveActions={[() => deleteClient(client.id), () => setDeleteClientOpen(false), () => navigate('/clients/dashboard')]}
        title="Delete client"
        description="This action will delete the client from the current group"
        content="If you choose to continue you'll no longer have access to this client - all sessions belonging to the client will also become innaccessible."
      />
    </div>
  )
}

export default ClientHeader;