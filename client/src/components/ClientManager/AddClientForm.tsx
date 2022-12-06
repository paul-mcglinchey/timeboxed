import { Formik } from "formik";
import { useClientService } from "../../hooks";
import { addClientValidationSchema } from "../../schema/clientValidationSchema";
import { FormikInput } from "..";
import { IContextualFormProps } from "../../models";
import { useState } from "react";
import { IApiError } from "../../models/error.model";
import { FormikForm } from "../Common/FormikForm";

const AddClientForm = ({ ContextualSubmissionButton }: IContextualFormProps) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()

  const { addClient } = useClientService(setIsLoading, setError)

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
        <FormikForm error={error} className="flex flex-1 flex-col space-y-8 text-gray-200">
          <FormikInput name="firstName" label="First name" errors={errors.firstName} touched={touched.firstName} />
          <FormikInput name="lastName" label="Last name" errors={errors.lastName} touched={touched.lastName} />
          <FormikInput name="primaryEmailAddress" label="Email" errors={errors.primaryEmailAddress} touched={touched.primaryEmailAddress} />
          {ContextualSubmissionButton('Add client', undefined, dirty && isValid, undefined, isLoading)}
        </FormikForm>
      )}
    </Formik>
  )
}

export default AddClientForm;