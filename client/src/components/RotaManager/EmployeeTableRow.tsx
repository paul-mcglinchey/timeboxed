import { useState } from "react"
import { useEmployeeService } from "../../hooks"
import { IEmployee } from "../../models"
import { IApiError } from "../../models/error.model"
import { InlineButton, Dialog, TableRow, TableRowItem, Modal, SpinnerLoader } from "../Common"
import UpdateEmployeeForm from "./UpdateEmployeeForm"

interface IEmployeeTableRowProps {
  employee: IEmployee
}

const EmployeeTableRow = ({ employee }: IEmployeeTableRowProps) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()

  const [deleteEmployeeOpen, setDeleteEmployeeOpen] = useState(false)
  const [editEmployeeOpen, setEditEmployeeOpen] = useState(false)

  const { deleteEmployee } = useEmployeeService(setIsLoading, setError)

  const handleDelete = async () => {
    deleteEmployee(employee.id)
    setDeleteEmployeeOpen(false)
  }

  return (
    <>
      {employee ? (
        <TableRow>
          <TableRowItem>
            <div className="flex flex-col">
              <div>{employee.firstName} {employee.lastName}</div>
              <div>{employee.primaryEmailAddress}</div>
            </div>
          </TableRowItem>
          <TableRowItem>
            <div className="flex flex-grow justify-end">
              <InlineButton action={() => setEditEmployeeOpen(true)} color="text-blue-500">
                Edit
              </InlineButton>
              <InlineButton action={() => setDeleteEmployeeOpen(true)} color="text-red-500">
                Delete
              </InlineButton>
              <Dialog
                isOpen={deleteEmployeeOpen}
                close={() => setDeleteEmployeeOpen(false)}
                positiveAction={handleDelete}
                title="Delete employee"
                description="This action will delete the employee from the current group"
                content="If you choose to continue you'll no longer have access to this employee in future schedules - this won't affect schedules created in the past."
              />
              <Modal
                title="Update employee"
                description="This dialog can be used to update existing employees"
                isOpen={editEmployeeOpen}
                close={() => setEditEmployeeOpen(false)}
              >
                {(ConfirmationButton) => (
                  <UpdateEmployeeForm employee={employee} ContextualSubmissionButton={ConfirmationButton} />
                )}
              </Modal>
            </div>
          </TableRowItem>
        </TableRow>
      ) : (
        isLoading ? <SpinnerLoader /> : error ? Error : null
      )}
    </>
  )
}

export default EmployeeTableRow;