import { useEffect, useState } from 'react';
import { TrashIcon, EllipsisVerticalIcon, PencilIcon, HeartIcon as HeartSolid } from '@heroicons/react/24/solid';
import { HeartIcon } from '@heroicons/react/24/outline';
import { IGroup } from '../../models';
import { useAuthService, useGroupService, useNotification } from '../../hooks';
import { Dialog, SpinnerIcon, SquareIconButton } from '../Common';
import DataPoint from './DataPoint';
import GroupModal from './GroupModal';
import { IApiError } from '../../models/error.model';
import { Notification } from '../../enums';

interface IGroupCardProps {
  g: IGroup
}

const GroupCard = ({ g }: IGroupCardProps) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()

  const [cardFlipped, setCardFlipped] = useState(false);
  const [deleteGroupOpen, setDeleteGroupOpen] = useState(false);
  const [editGroupOpen, setEditGroupOpen] = useState(false);

  const toggleCardFlipped = () => setCardFlipped(!cardFlipped);

  const { deleteGroup } = useGroupService(setIsLoading, setError)
  const { user, updatePreferences } = useAuthService(setIsLoading, setError)
  const { addNotification } = useNotification()

  const isDefaultGroup = () => user?.preferences?.defaultGroup === g.id

  const toggleDefaultGroup = () => {
    updatePreferences({ defaultGroup: isDefaultGroup() ? undefined : g.id })
  }

  const handleDelete = async () => {
    deleteGroup(g.id)
    setDeleteGroupOpen(false)
  }

  useEffect(() => {
    if (error) addNotification(error.message, Notification.Error) 
  }, [error, addNotification])

  return (
    <>
      <div 
        style={{ color: g.colour ?? undefined }} 
        className={`p-4 h-full rounded-lg transform hover:scale-100-1/2 transition-all bg-gray-300 dark:bg-slate-800`}
      >
        <div className="flex flex-grow justify-between items-center space-x-4">
          <h1 className="text-xl md:text-3xl font-extrabold tracking-wide mr-10">{g.name}</h1>
          <div className="flex space-x-4">
            {isLoading && <SpinnerIcon className="h-5 w-5" />}
            <SquareIconButton Icon={isDefaultGroup() ? HeartSolid : HeartIcon} action={() => toggleDefaultGroup()} />
            <SquareIconButton Icon={PencilIcon} action={() => setEditGroupOpen(true)} />
            <SquareIconButton Icon={EllipsisVerticalIcon} action={() => toggleCardFlipped()} className={`transform transition-all duration-500 ${cardFlipped ? 'rotate-180' : 'rotate-0'}`} />
          </div>
        </div>
        <div className="flex justify-between items-end my-4">
          <div>
            {cardFlipped ? <DataPoint value={g.users?.length || 0} label="user" /> : <DataPoint value={g.applications?.length} label="application" />}
          </div>
          {cardFlipped && (
            <div>
              <SquareIconButton Icon={TrashIcon} action={() => setDeleteGroupOpen(true)} />
            </div>
          )}
        </div>
      </div>
      <GroupModal isOpen={editGroupOpen} close={() => setEditGroupOpen(false)} groupId={g.id} />
      <Dialog
        isOpen={deleteGroupOpen}
        close={() => setDeleteGroupOpen(false)}
        positiveAction={handleDelete}
        title="Delete group"
        description="This action will delete the group for all users"
        content="If you choose to continue you and all other users of this group will no longer have access to it or any of it's application data"
      />
    </>
  )
}

export default GroupCard;