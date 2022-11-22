import { useState } from 'react';
import { TrashIcon, DotsVerticalIcon, PencilIcon, HeartIcon as HeartSolid } from '@heroicons/react/solid';
import { HeartIcon } from '@heroicons/react/outline';
import { IGroup } from '../../models';
import { useAuthService, useGroupService } from '../../hooks';
import { Dialog, SquareIconButton } from '../Common';
import { GroupModal, DataPoint } from '.';

interface IGroupCardProps {
  g: IGroup
}

const GroupCard = ({ g }: IGroupCardProps) => {

  const [cardFlipped, setCardFlipped] = useState(false);
  const [deleteGroupOpen, setDeleteGroupOpen] = useState(false);
  const [editGroupOpen, setEditGroupOpen] = useState(false);

  const toggleCardFlipped = () => setCardFlipped(!cardFlipped);

  const { deleteGroup } = useGroupService()
  const { user, updatePreferences } = useAuthService()

  const isDefaultGroup = () => user?.preferences?.defaultGroup === g.id

  const toggleDefaultGroup = () => {
    updatePreferences({ defaultGroup: isDefaultGroup() ? undefined : g.id })
  }

  return (
    <>
      <div 
        style={{ color: g.colour ?? undefined }} 
        className={`p-4 h-full rounded-lg transform hover:scale-100-1/2 transition-all bg-gray-300 dark:bg-slate-800`}
      >
        <div className="flex flex-grow justify-between items-center space-x-4">
          <h1 className="text-xl md:text-3xl font-extrabold tracking-wide mr-10">{g.name}</h1>
          <div className="flex space-x-4">
            <SquareIconButton Icon={isDefaultGroup() ? HeartSolid : HeartIcon} action={() => toggleDefaultGroup()} />
            <SquareIconButton Icon={PencilIcon} action={() => setEditGroupOpen(true)} />
            <SquareIconButton Icon={DotsVerticalIcon} action={() => toggleCardFlipped()} className={`transform transition-all duration-500 ${cardFlipped ? 'rotate-180' : 'rotate-0'}`} />
          </div>
        </div>
        <div className="flex justify-between items-end my-4">
          <div>
            {cardFlipped ? <DataPoint value={g.groupUsers?.length || 0} label="user" /> : <DataPoint value={g.applications?.length} label="application" />}
          </div>
          {cardFlipped && (
            <div>
              <SquareIconButton Icon={TrashIcon} action={() => setDeleteGroupOpen(true)} />
            </div>
          )}
        </div>
      </div>
      <GroupModal isOpen={editGroupOpen} close={() => setEditGroupOpen(false)} group={g} />
      <Dialog
        isOpen={deleteGroupOpen}
        close={() => setDeleteGroupOpen(false)}
        positiveActions={[() => deleteGroup(g.id), () => setDeleteGroupOpen(false)]}
        title="Delete group"
        description="This action will delete the group for all users"
        content="If you choose to continue you and all other users of this group will no longer have access to it or any of it's application data"
      />
    </>
  )
}

export default GroupCard;