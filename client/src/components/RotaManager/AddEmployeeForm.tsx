import { Formik } from "formik"
import { useState } from "react";
import { useEmployeeService } from "../../hooks";
import { IContextualFormProps } from "../../models"
import { IApiError } from "../../models/error.model";
import { addEmployeeValidationSchema } from "../../schema/employeeValidationSchema";
import { FormikInput } from "../Common";
import { FormikForm } from "../Common/FormikForm";

const AddEmployeeForm = ({ ContextualSubmissionButton }: IContextualFormProps) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()

  const { addEmployee } = useEmployeeService(setIsLoading, setError)

  return (
    <Formik
      initialValues={{
        firstName: '',
        lastName: '',
        primaryEmailAddress: ''
      }}
      validationSchema={addEmployeeValidationSchema}
      onSubmit={(values) => {
        addEmployee(values)
      }}
    >
      {({ errors, touched }) => (
        <FormikForm error={error}>
          <FormikInput name="firstName" label="First name" errors={errors.firstName} touched={touched.firstName} />
          <FormikInput name="lastName" label="Last name" errors={errors.lastName} touched={touched.lastName} />
          <FormikInput name="primaryEmailAddress" label="Email" errors={errors.primaryEmailAddress} touched={touched.primaryEmailAddress} />

          {ContextualSubmissionButton('Add employee', undefined, undefined, isLoading)}
        </FormikForm>
      )}
    </Formik>
  )
}

export default AddEmployeeForm;