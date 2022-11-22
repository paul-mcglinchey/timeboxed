import { Form, Formik } from "formik"
import { useEmployeeService } from "../../hooks";
import { IContextualFormProps } from "../../models"
import { addEmployeeValidationSchema } from "../../schema/employeeValidationSchema";
import { StyledField } from "../Common";

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
        <Form className="flex grow md:flex-1 flex-col space-y-8 text-gray-200">
          <div className="flex flex-col space-y-4 md:space-y-0 md:flex-row md:space-x-4 items-center">
            <StyledField compact name="firstName" label="First name" errors={errors.firstName} touched={touched.firstName} />
            <StyledField compact name="lastName" label="Last name" errors={errors.lastName} touched={touched.lastName} />
            <StyledField compact name="primaryEmailAddress" label="Email" errors={errors.primaryEmailAddress} touched={touched.primaryEmailAddress} />
          </div>
          {ContextualSubmissionButton()}
        </Form>
      )}
    </Formik>
  )
}

export default AddEmployeeForm;