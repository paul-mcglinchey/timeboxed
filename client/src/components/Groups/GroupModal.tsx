import { Modal } from "../Common";
import { IGroup } from "../../models";

import AddGroupForm from "./AddGroupForm";
import UpdateGroupForm from "./UpdateGroupForm";

interface IGroupModalProps {
  isOpen: boolean,
  close: () => void,
  group?: IGroup
}

const GroupModal = ({ isOpen, close, group }: IGroupModalProps) => {
  return (
    <Modal
      title={group ? 'Edit Group' : 'Add Group'}
      description={`This dialog can be used to ${group ? 'edit an existing' : 'create a new'} group`}
      isOpen={isOpen}
      close={close}
    >
      {(ConfirmationButton) => (
        group ? (
          <UpdateGroupForm group={group} ContextualSubmissionButton={ConfirmationButton} />
        ) : (
          <AddGroupForm ContextualSubmissionButton={ConfirmationButton} />
        )
      )}
    </Modal>
  )
}

export default GroupModal;