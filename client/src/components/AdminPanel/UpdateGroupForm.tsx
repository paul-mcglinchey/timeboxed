import { Formik } from "formik"
import { FormSection } from ".."
import { IContextualFormProps, IGroup } from "../../models"
import { FormikForm } from "../Common/FormikForm"
import ApplicationMultiSelector from "../Groups/ApplicationMultiSelector"
import { useGroupService } from "../../hooks"
import { useState } from "react"
import { IApiError } from "../../models/error.model"

interface IUpdateGroupFormProps extends IContextualFormProps {
  group: IGroup
  submissionAction: () => Promise<void> | void
}

const UpdateGroupForm = ({ ContextualSubmissionButton, group, submissionAction }: IUpdateGroupFormProps) => {

  const [loading, setLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError | undefined>()

  const { adminUpdateGroup } = useGroupService(setLoading, setError)

  return (
    <Formik
      enableReinitialize
      initialValues={{
        applications: group.applications || [],
      }}
      onSubmit={async (values) => {
        console.log({ ...group, ...values })
        await adminUpdateGroup({ ...group, ...values }, group.id)
        await submissionAction()
      }}
    >
      {({ values, setFieldValue, isValid }) => (
        <FormikForm error={error}>
          <FormSection title="Group Applications" classes="mb-6">
            <ApplicationMultiSelector
              formValues={values.applications}
              setFieldValue={(a) => setFieldValue('applications', a)}
            />
          </FormSection>
          {ContextualSubmissionButton('Save changes', undefined, isValid, loading)}
        </FormikForm>
      )}
    </Formik >
  )
}

export default UpdateGroupForm