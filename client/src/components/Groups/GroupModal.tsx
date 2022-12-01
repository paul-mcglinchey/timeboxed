import { Modal } from "../Common";
import { AddGroupForm, UpdateGroupForm } from ".";
import { IGroup } from "../../models";
import { UserProvider } from "../../contexts";

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
          <UserProvider groupId={group.id}>
            <UpdateGroupForm group={group} ContextualSubmissionButton={ConfirmationButton} />
          </UserProvider>
        ) : (
          <AddGroupForm ContextualSubmissionButton={ConfirmationButton} />
        )
      )}
    </Modal>
  )
}

export default GroupModal;