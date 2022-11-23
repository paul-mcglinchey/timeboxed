import { Form, Formik } from "formik";
import { useClientService } from "../../hooks";
import { addClientValidationSchema } from "../../schema/clientValidationSchema";
import { FormikInput } from "..";
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
          <FormikInput name="firstName" label="First name" errors={errors.firstName} touched={touched.firstName} />
          <FormikInput name="lastName" label="Last name" errors={errors.lastName} touched={touched.lastName} />
          <FormikInput name="primaryEmailAddress" label="Email" errors={errors.primaryEmailAddress} touched={touched.primaryEmailAddress} />
          {ContextualSubmissionButton('Add client', undefined, dirty && isValid)}
        </Form>
      )}
    </Formik>
  )
}

export default AddClientForm;