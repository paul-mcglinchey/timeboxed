import { Modal } from "../Common";
import { GroupForm } from ".";
import { IGroup } from "../../models";

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
        <GroupForm group={group} ContextualSubmissionButton={ConfirmationButton} />
      )}
    </Modal>
  )
}

export default GroupModal;