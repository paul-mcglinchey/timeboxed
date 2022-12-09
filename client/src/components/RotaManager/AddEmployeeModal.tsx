import { IEmployee } from "../../models"
import { Modal } from "../Common"
import AddEmployeeForm from "./AddEmployeeForm"

interface IAddEmployeeModalProps {
  employee?: IEmployee | undefined
  isOpen: boolean,
  close: () => void
  level?: 1 | 2 | 3
}

const AddEmployeeModal = ({ employee, isOpen, close, level = 1 }: IAddEmployeeModalProps) => {
  return (
    <Modal
      title="Add employee"
      description="This dialog can be used to create new employees"
      isOpen={isOpen}
      close={close}
      level={level}
      allowAddMultiple={!employee}
    >
      {(ConfirmationButton) => (
        <AddEmployeeForm ContextualSubmissionButton={ConfirmationButton}/>
      )}
    </Modal>
  )
}

export default AddEmployeeModal;