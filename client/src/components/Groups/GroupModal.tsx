import { Modal } from "../Common";

import AddGroupForm from "./AddGroupForm";
import UpdateGroupForm from "./UpdateGroupForm";

interface IGroupModalProps {
  isOpen: boolean,
  close: () => void,
  groupId?: string
}

const GroupModal = ({ isOpen, close, groupId }: IGroupModalProps) => {
  return (
    <Modal
      title={groupId ? 'Edit Group' : 'Add Group'}
      description={`This dialog can be used to ${groupId ? 'edit an existing' : 'create a new'} group`}
      isOpen={isOpen}
      close={close}
    >
      {(ConfirmationButton) => (
        groupId ? (
          <UpdateGroupForm groupId={groupId} ContextualSubmissionButton={ConfirmationButton} />
        ) : (
          <AddGroupForm ContextualSubmissionButton={ConfirmationButton} />
        )
      )}
    </Modal>
  )
}

export default GroupModal;