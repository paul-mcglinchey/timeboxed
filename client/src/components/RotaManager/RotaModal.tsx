import { Modal } from "../Common";
import AddRotaForm from "./AddRotaForm";
import UpdateRotaForm from "./RotaForm";

interface IRotaProps {
  isOpen: boolean
  close: () => void
  rotaId?: string
}

const RotaModal = ({ isOpen, close, rotaId }: IRotaProps) => {
  return (
    <Modal 
      title="Add rota"
      description="This dialog can be used to create a new rota" 
      isOpen={isOpen} 
      close={close}
    >
      {(ConfirmSubmission) => (
        rotaId ? <UpdateRotaForm rotaId={rotaId} ContextualSubmissionButton={ConfirmSubmission} /> : <AddRotaForm ContextualSubmissionButton={ConfirmSubmission} />
      )}
    </Modal>
  ) 
}

export default RotaModal;