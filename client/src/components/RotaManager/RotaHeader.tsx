import { Dispatch, SetStateAction, useState } from "react";
import { Link } from "react-router-dom";
import { EyeIcon, LockClosedIcon, LockOpenIcon, PencilIcon, TrashIcon, UsersIcon } from "@heroicons/react/solid";
import { useFormikContext } from "formik";
import { IRota } from "../../models";
import { useAuthService, useRotaService } from "../../hooks";
import { Button, Dialog, Dropdown } from "../Common";
import { RotaModal } from ".";
import { Application, Permission } from "../../enums";

interface IRotaHeaderProps {
  rota: IRota,
  editing: boolean,
  setEditing: Dispatch<SetStateAction<boolean>>
}

const RotaHeader = ({ rota, editing, setEditing }: IRotaHeaderProps) => {

  const [deletionOpen, setDeletionOpen] = useState<boolean>(false);
  const [editRotaOpen, setEditRotaOpen] = useState<boolean>(false);

  const { updateRota, deleteRota } = useRotaService()
  const { handleSubmit, dirty } = useFormikContext()
  const { hasPermission } = useAuthService()

  const updateEditingStatus = () => {
    dirty && handleSubmit()
    setEditing(!editing)
  }

  const updateLockedStatus = () => {
    updateRota(rota?.id, { ...rota, locked: !rota?.locked })
    setEditing(false)
  }

  return (
    <div className="flex justify-between items-center text-gray-200">
      <Link to="/rotas/dashboard" className="flex sm:hidden rounded-lg">
        <Button content="Back to rotas" buttonType="Tertiary" />
      </Link>
      <div className="hidden sm:flex items-center pb-0.5 space-x-3 text-white text-base md:text-2xl font-semibold tracking-wider">
        <Link to="/rotas/dashboard">
          <span className="rounded-lg bg-gray-800 px-2 pb-1">Rotas</span>
        </Link>
        <span> / </span>
        <span className="text-green-500">
          <span>{rota.name}</span>
        </span>
        <span> / {editing ? 'Editing' : 'Viewing'}</span>
      </div>
      <div className="flex space-x-4 items-center">
        {rota.locked && (
          <LockClosedIcon className="text-gray-400 w-6 h-6" />
        )}
        {hasPermission(Application.RotaManager, Permission.AddEditDeleteRotas) && (
          <>
            {!rota.locked && (
              <Button buttonType="Tertiary" content={editing ? 'View' : 'Edit'} Icon={editing ? EyeIcon : PencilIcon} action={() => updateEditingStatus()} />
            )}
            <Dropdown options={[
              { label: 'Edit Rota Details', action: () => setEditRotaOpen(true), Icon: PencilIcon },
              { label: 'Synchronize Employees', action: () => { }, Icon: UsersIcon },
              { label: 'Modify Employees', action: () => { }, Icon: UsersIcon },
              { label: rota.locked ? 'Unlock rota' : 'Lock rota', action: () => updateLockedStatus(), Icon: rota.locked ? LockOpenIcon : LockClosedIcon },
              { label: 'Delete', action: () => setDeletionOpen(true), Icon: TrashIcon },
            ]} />
          </>
        )}
      </div>
      <RotaModal isOpen={editRotaOpen} close={() => setEditRotaOpen(false)} rota={rota} />
      <Dialog
        isOpen={deletionOpen}
        close={() => setDeletionOpen(false)}
        positiveActions={[() => deleteRota(rota.id)]}
        title="Delete rota"
        description="This action will delete the rota from the current group"
        content="If you choose to continue you'll no longer have access to this rota or any of the schedules belonging to it"
      />
    </div>
  )
}

export default RotaHeader;