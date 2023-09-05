import { Formik } from "formik";
import { useClientService } from "../../hooks";
import { addClientValidationSchema } from "../../schema/clientValidationSchema";
import { FormikInput } from "..";
import { IContextualFormProps } from "../../models";
import { useState } from "react";
import { IApiError } from "../../models/error.model";
import { FormikForm } from "../Common/FormikForm";
import FormGrouping from "../Common/FormGrouping";

const AddClientForm = ({ ContextualSubmissionButton }: IContextualFormProps) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()

  const { addClient, fetchClients } = useClientService(setIsLoading, setError)

  return (
    <Formik
      initialValues={{
        firstName: '',
        lastName: '',
        primaryEmailAddress: ''
      }}
      validationSchema={addClientValidationSchema}
      onSubmit={async (values, actions) => {
        await addClient(values)
        await fetchClients()
        actions.resetForm()
      }}
    >
      {({ errors, touched, dirty, isValid }) => (
        <FormikForm error={error}>
          <FormGrouping>
            <FormikInput name="firstName" label="First name" errors={errors.firstName} touched={touched.firstName} />
            <FormikInput name="lastName" label="Last name" errors={errors.lastName} touched={touched.lastName} />
            <FormikInput name="primaryEmailAddress" label="Email" errors={errors.primaryEmailAddress} touched={touched.primaryEmailAddress} />
          </FormGrouping>
          {ContextualSubmissionButton('Add client', undefined, dirty && isValid, isLoading)}
        </FormikForm>
      )}
    </Formik>
  )
}

export default AddClientForm;