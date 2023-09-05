import { Formik } from "formik";
import { useGroupService } from "../../hooks";
import { IContextualFormProps } from "../../models";
import { generateColour } from "../../services";
import { groupValidationSchema } from "../../schema";
import { ColourPicker, FormSection, FormikInput, FormikTextArea } from "../Common";
import { FormikForm } from "../Common/FormikForm";
import { useState } from "react";
import { IApiError } from "../../models/error.model";
import FormGrouping from "../Common/FormGrouping";

const AddGroupForm = ({ ContextualSubmissionButton }: IContextualFormProps) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()

  const { addGroup } = useGroupService(setIsLoading, setError)

  return (
    <Formik
      initialValues={{
        name: '',
        description: '',
        colour: generateColour()
      }}
      validationSchema={groupValidationSchema}
      onSubmit={(values) => {
        addGroup({ ...values })
      }}
    >
      {({ errors, touched, values, setFieldValue, isValid }) => (
        <FormikForm error={error}>
          <FormSection title="Details">
            <FormGrouping>
              <div className="flex items-end space-x-2">
                <FormikInput name="name" label="Groupname" errors={errors.name} touched={touched.name} classes="flex flex-grow" />
                <ColourPicker square colour={values.colour} setColour={(pc) => setFieldValue('colour', pc)} />
              </div>
              <FormikTextArea name="description" label="Description" errors={errors.description} touched={touched.description} />
            </FormGrouping>
          </FormSection>
          {ContextualSubmissionButton('Create group', undefined, isValid, isLoading)}
        </FormikForm>
      )
      }
    </Formik >
  )
}

export default AddGroupForm;