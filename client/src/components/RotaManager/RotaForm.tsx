import { useState } from 'react';
import { Formik, Form } from 'formik';
import { ExternalLinkIcon } from '@heroicons/react/solid';
import { useRotaService } from '../../hooks';
import { IContextualFormProps, IRota } from '../../models';
import { rotaValidationSchema } from '../../schema';
import { FormikInput, FormSection, SpinnerLoader } from '../Common';
import { EmployeeMultiSelector, AddEmployeeModal } from '.';
import { generateColour } from '../../services';
import { IApiError } from '../../models/error.model';

interface IRotaFormProps {
  rota?: IRota | undefined
}

const RotaForm = ({ rota, ContextualSubmissionButton }: IRotaFormProps & IContextualFormProps) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()

  const [addEmployeesOpen, setAddEmployeesOpen] = useState(false)

  const { addRota, updateRota } = useRotaService(setIsLoading, setError)

  return (
    <>
      {rota ? (
        <>
        <Formik
          initialValues={{
            name: rota.name || '',
            description: rota.description || '',
            closingHour: rota.closingHour || 22,
            employees: rota.employees || [],
            colour: generateColour()
          }}
          validationSchema={rotaValidationSchema}
          onSubmit={(values) => {
            rota ? updateRota(rota.id, values) : addRota(values)
          }}
        >
          {({ values, errors, touched, setFieldValue }) => (
            <Form>
              <FormSection title="Details">
              <div className="grid grid-cols-5 gap-2">
                <FormikInput name="name" label="Name" errors={errors.name} touched={touched.name} classes="col-span-4"/>
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
                {ContextualSubmissionButton()}
              </div>
            </Form>
          )}
        </Formik>
        <AddEmployeeModal isOpen={addEmployeesOpen} close={() => setAddEmployeesOpen(false)} level={2} />
        </>
      ) : (
        isLoading ? <SpinnerLoader /> : error ? Error : null
      )}
    </>
  )
}

export default RotaForm;