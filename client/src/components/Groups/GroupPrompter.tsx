import { UserGroupIcon } from '@heroicons/react/solid';
import { Prompter } from '../Common';

const GroupPrompter = ({ action }: { action: () => void }) => {
  return (
    <Prompter
      Icon={UserGroupIcon}
      title="Create a group to get started"
      action={action}
    />
  )
}

export default GroupPrompter;