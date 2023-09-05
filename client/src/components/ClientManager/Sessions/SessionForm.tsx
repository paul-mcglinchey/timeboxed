import { useCallback, useEffect, useState } from "react"
import { FormikInput, FormikTextArea, TagInput } from "../.."
import { IGroupClientTagResponse, ITag } from "../../../models"
import { FormikProps } from "formik"
import { useClientService } from "../../../hooks"
import { IApiError } from "../../../models/error.model"
import FormGrouping from "../../Common/FormGrouping"

interface ISessionForm {
  title: string
  description: string
  sessionDate: string
  tags: ITag[]
}

interface ISessionFormProps {
  formik: FormikProps<ISessionForm>
}

const SessionForm = ({ formik: { errors, touched, values, setFieldValue } }: ISessionFormProps) => {

  const [loading, setLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError | undefined>()
  const [groupClientTags, setGroupClientTags] = useState<IGroupClientTagResponse[]>([])
  
  const { getGroupClientTags } = useClientService(setLoading, setError)

  const fetchTags = useCallback(async () => {
    let tags = await getGroupClientTags()
    setGroupClientTags(tags)
  }, [])

  useEffect(() => {
    fetchTags()

    if (error) console.error(error.message)
  }, [fetchTags, error])

  return (
    <FormGrouping>
      <FormikInput name="title" label="Title" errors={errors.title} touched={touched.title}/>
      <FormikTextArea name="description" label="Description" errors={errors.description} touched={touched.description} />
      <FormikInput id="sessiondate" type="date" name="sessionDate" label="Session Date" errors={errors.sessionDate} touched={touched.sessionDate} />
      <TagInput
        name="tags"
        tags={values.tags}
        availableTags={groupClientTags}
        label="Tags"
        update={(tags) => setFieldValue('tags', tags)}
        errors={errors.tags}
        touched={touched.tags}
        loading={loading}
        helperMessage="You can add up to 5 tags to a session, the tags can then be used to filter your clients and sessions"
      />
    </FormGrouping>
  )
}

export default SessionForm