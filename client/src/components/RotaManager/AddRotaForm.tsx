import { ExternalLinkIcon } from "@heroicons/react/solid"
import { Formik } from "formik"
import { useState } from "react"
import { useRotaService } from "../../hooks"
import { IContextualFormProps } from "../../models"
import { IApiError } from "../../models/error.model"
import { rotaValidationSchema } from "../../schema"
import { generateColour } from "../../services"
import { FormSection, FormikInput } from "../Common"
import { FormikForm } from "../Common/FormikForm"
import AddEmployeeModal from "./AddEmployeeModal"
import EmployeeMultiSelector from "./EmployeeMultiSelector"

const AddRotaForm = ({ ContextualSubmissionButton }: IContextualFormProps) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()

  const [addEmployeesOpen, setAddEmployeesOpen] = useState(false)

  const { addRota } = useRotaService(setIsLoading, setError)

  return (
    <>
      <Formik
        initialValues={{
          name: '',
          description: '',
          closingHour: 22,
          employees: [],
          colour: generateColour()
        }}
        validationSchema={rotaValidationSchema}
        onSubmit={(values) => {
          addRota(values)
        }}
      >
        {({ values, errors, touched, setFieldValue }) => (
          <FormikForm error={error}>
            <FormSection title="Details">
              <div className="grid grid-cols-5 gap-2">
                <FormikInput name="name" label="Name" errors={errors.name} touched={touched.name} classes="col-span-4" />
                <FormikInput type="number" name="closingHour" label="Closing hour" errors={errors.closingHour} touched={touched.closingHour} />
              </div>
              <FormikInput as="textarea" name="description" label="Description" errors={errors.description} touched={touched.description} />
            </FormSection>
            <div className="flex flex-col space-y-4">
              <FormSection title="Employees">
                <div className="flex flex-col space-y-4 flex-grow rounded">
                  <EmployeeMultiSelector formValues={values.employees} setFieldValue={(e) => setFieldValue('employees', e)} />
                </div>
              </FormSection>
            </div>
            <div className="flex justify-between">
              <button
                onClick={() => setAddEmployeesOpen(true)}
                type="button"
                className="flex items-center space-x-2 uppercase text-sm text-gray-300 hover:bg-gray-900 px-4 py-1 rounded-lg transition-all font-bold"
              >
                <span>Add employees</span>
                <ExternalLinkIcon className="w-5 h-5" />
              </button>
              {ContextualSubmissionButton('Add Rota', undefined, undefined, undefined, isLoading)}
            </div>
          </FormikForm>
        )}
      </Formik>
      <AddEmployeeModal isOpen={addEmployeesOpen} close={() => setAddEmployeesOpen(false)} level={2} />
    </>
  )
}

export default AddRotaForm;