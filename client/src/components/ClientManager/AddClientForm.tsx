import { Form, Formik } from "formik";
import { useClientService } from "../../hooks";
import { addClientValidationSchema } from "../../schema/clientValidationSchema";
import { StyledField } from "..";
import { IContextualFormProps } from "../../models";

const AddClientForm = ({ ContextualSubmissionButton }: IContextualFormProps) => {

  const { addClient } = useClientService()

  return (
    <Formik
      initialValues={{
        firstName: null,
        lastName: null,
        primaryEmailAddress: null
      }}
      validationSchema={addClientValidationSchema}
      onSubmit={(values) => {
        addClient(values)
      }}
    >
      {({ errors, touched, dirty, isValid }) => (
        <Form className="flex flex-1 flex-col space-y-8 text-gray-200">
          <div className="flex flex-col space-y-4 md:space-y-0 md:flex-row md:space-x-4 items-center">
            <StyledField compact name="firstName" label="First name" errors={errors.firstName} touched={touched.firstName} />
            <StyledField compact name="lastName" label="Last name" errors={errors.lastName} touched={touched.lastName} />
            <StyledField compact name="primaryEmailAddress" label="Email" errors={errors.primaryEmailAddress} touched={touched.primaryEmailAddress} />
          </div>
          {ContextualSubmissionButton('Add client', undefined, dirty && isValid)}
        </Form>
      )}
    </Formik>
  )
}

export default AddClientForm;