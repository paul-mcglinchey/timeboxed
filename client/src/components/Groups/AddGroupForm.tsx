import { Formik } from "formik";
import { useGroupService } from "../../hooks";
import { IContextualFormProps } from "../../models";
import { generateColour } from "../../services";
import { groupValidationSchema } from "../../schema";
import { ColourPicker, FormSection, FormikInput } from "../Common";
import { FormikForm } from "../Common/FormikForm";
import ApplicationMultiSelector from "./ApplicationMultiSelector";
import { useState } from "react";
import { IApiError } from "../../models/error.model";

const AddGroupForm = ({ ContextualSubmissionButton }: IContextualFormProps) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()

  const { addGroup } = useGroupService(setIsLoading, setError)

  return (
    <Formik
      initialValues={{
        name: '',
        description: '',
        applications: [],
        colour: generateColour()
      }}
      validationSchema={groupValidationSchema}
      onSubmit={(values) => {
        addGroup(values)
      }}
    >
      {({ errors, touched, values, setFieldValue, isValid }) => (
        <FormikForm error={error}>
          <FormSection title="Details">
            <div className="flex items-end space-x-2">
              <FormikInput name="name" label="Groupname" errors={errors.name} touched={touched.name} classes="flex flex-grow" />
              <ColourPicker square colour={values.colour} setColour={(pc) => setFieldValue('colour', pc)} />
            </div>
            <FormikInput as="textarea" name="description" label="Description" errors={errors.description} touched={touched.description} />
          </FormSection>
          <FormSection title="Group Applications" classes="mb-6">
            <ApplicationMultiSelector
              formValues={values.applications}
              setFieldValue={(a) => setFieldValue('applications', a)}
            />
          </FormSection>
          {ContextualSubmissionButton('Create group', undefined, isValid, true, isLoading)}
        </FormikForm>
      )
      }
    </Formik >
  )
}

export default AddGroupForm;