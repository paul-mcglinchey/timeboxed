import { Modal } from "../Common"
import GroupInvites from "./GroupInvites"

interface IGroupInvitesModalProps {
  isOpen: boolean
  close: () => void
}

const GroupInvitesModal = ({ isOpen, close }: IGroupInvitesModalProps) => {
  return (
    <Modal
      title="Group invites"
      description="This dialog allows for pending group invites to be viewed and managed."
      isOpen={isOpen}
      close={close}
    >
      {() => (
        <GroupInvites />
      )}
    </Modal>
  )
}

export default GroupInvitesModal