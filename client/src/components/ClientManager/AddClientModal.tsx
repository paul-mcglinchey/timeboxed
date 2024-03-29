import { Modal } from ".."
import { IClient } from "../../models"
import AddClientForm from './AddClientForm'

interface IAddClientModalProps {
  isOpen: boolean,
  close: () => void,
  client?: IClient,
  compact?: boolean
}

const AddClientModal = ({ isOpen, close, client }: IAddClientModalProps) => {
  return (
    <Modal 
      title="Add client"
      description="This dialog can be used to create new clients" 
      isOpen={isOpen}
      close={close}
      allowAddMultiple={!client}
    >
      {(ConfirmationButton) => (
        <AddClientForm ContextualSubmissionButton={ConfirmationButton} />
      )}
    </Modal>
  )
}

export default AddClientModal;