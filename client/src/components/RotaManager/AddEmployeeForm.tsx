import { Form, Formik } from "formik"
import { useEmployeeService } from "../../hooks";
import { IContextualFormProps } from "../../models"
import { addEmployeeValidationSchema } from "../../schema/employeeValidationSchema";
import { FormikInput } from "../Common";

const AddEmployeeForm = ({ ContextualSubmissionButton }: IContextualFormProps) => {

  const { addEmployee } = useEmployeeService()

  return (
    <Formik
      initialValues={{
        firstName: null,
        lastName: null,
        primaryEmailAddress: null
      }}
      validationSchema={addEmployeeValidationSchema}
      onSubmit={(values) => {
        addEmployee(values)
      }}
    >
      {({ errors, touched }) => (
        <Form>
          <FormikInput name="firstName" label="First name" errors={errors.firstName} touched={touched.firstName} />
          <FormikInput name="lastName" label="Last name" errors={errors.lastName} touched={touched.lastName} />
          <FormikInput name="primaryEmailAddress" label="Email" errors={errors.primaryEmailAddress} touched={touched.primaryEmailAddress} />
          {ContextualSubmissionButton()}
        </Form>
      )}
    </Formik>
  )
}

export default AddEmployeeForm;