import { useEffect, useState } from "react";
import { Form, Formik } from "formik";
import { IRota } from "../../models";
import { Modal, SpinnerLoader } from "../Common";
import { EmployeeMultiSelector } from "..";
import { useRotaService } from "../../hooks";
import { IApiError } from "../../models/error.model";

interface IEditRotaEmployeesProps {
  isOpen: boolean
  close: () => void
  rota: IRota
}

const EditRotaEmployeesModal = ({ isOpen, close, rota }: IEditRotaEmployeesProps) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()

  const { updateRota } = useRotaService(setIsLoading, setError)

  useEffect(() => {
    document.addEventListener('keydown', handleKeydown)

    return () => {
      document.removeEventListener('keydown', handleKeydown)
    }
  }, [])

  const handleKeydown = (e: KeyboardEvent) => {
    if (e.ctrlKey && e.key === 'k') {
      e.preventDefault()
      document.getElementById('filter')?.focus()
    }
  }

  return (
    <Modal
      title="Edit rota employees"
      description="This dialog can be used to modify the employees belonging to a rota"
      isOpen={isOpen}
      close={close}
    >
      {(ConfirmationButton) => (
        <>
          {rota ? (
            <Formik
              initialValues={{
                employees: rota.employees || []
              }}
              onSubmit={(values) => {
                updateRota(rota.id, { ...rota, ...values })
              }}
            >
              {({ values, setFieldValue }) => (
                <Form>
                  <EmployeeMultiSelector formValues={values.employees} setFieldValue={(e) => setFieldValue('employees', e)} />
                  {ConfirmationButton()}
                </Form>
              )}
            </Formik>
          ) : (
            isLoading ? <SpinnerLoader /> : error ? Error : null
          )}
        </>
      )}
    </Modal>
  )
}

export default EditRotaEmployeesModal