import { IRota } from "../../models";
import { Modal } from "../Common";
import { RotaForm } from ".";

interface IRotaProps {
  isOpen: boolean
  close: () => void
  rota?: IRota
}

const RotaModal = ({ isOpen, close, rota }: IRotaProps) => {
  return (
    <Modal 
      title="Add rota"
      description="This dialog can be used to create a new rota" 
      isOpen={isOpen} 
      close={close}
    >
      {(ConfirmSubmission) => (
        <RotaForm rota={rota} ContextualSubmissionButton={ConfirmSubmission} />
      )}
    </Modal>
  ) 
}

export default RotaModal;