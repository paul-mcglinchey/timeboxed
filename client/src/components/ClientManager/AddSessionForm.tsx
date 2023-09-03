import { Formik } from 'formik';
import { sessionValidationSchema } from '../../schema';
import { IClientListResponse, IContextualFormProps, IGroupClientTagResponse } from '../../models';
import { useClientService } from '../../hooks';
import { CustomDate, FormikInput, FormikTextArea, TagInput } from '..';
import { IApiError } from '../../models/error.model';
import { FormikForm } from '../Common/FormikForm';
import { useCallback, useEffect, useState } from 'react';

const currentDate = new Date();
const currentDateAsString = currentDate.toISOString().split('T')[0];

interface IAddSessionFormProps {
  client: IClientListResponse
  submissionAction?: () => Promise<void> | void
}

const AddSessionForm = ({ client, submissionAction, ContextualSubmissionButton }: IAddSessionFormProps & IContextualFormProps) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()
  const [clientGroupTags, setGroupClientTags] = useState<IGroupClientTagResponse[]>([])

  const { addSession, getGroupClientTags } = useClientService(setIsLoading, setError)

  const fetchTags = useCallback(async () => {
    const tags = await getGroupClientTags()
    setGroupClientTags(tags)
  }, [])

  useEffect(() => {
    fetchTags()
  }, [fetchTags])

  return (
    <Formik
      initialValues={{
        title: '',
        description: '',
        sessionDate: currentDateAsString || '',
        tags: []
      }}
      validationSchema={sessionValidationSchema}
      onSubmit={async (values) => {
        await addSession(client.id, { ...values });
        submissionAction && await submissionAction()
      }}
    >
      {({ values, errors, touched, dirty, isValid, setFieldValue }) => (
        <FormikForm error={error}>
          <FormikInput name="title" label="Title" errors={errors.title} touched={touched.title} />
          <FormikTextArea name="description" label="Description" errors={errors.description} touched={touched.description} />
          <FormikInput id="sessiondate" type="date" name="sessionDate" label="Session Date" errors={errors.sessionDate} touched={touched.sessionDate} component={CustomDate} />
          <TagInput name="tags" tags={values.tags} availableTags={clientGroupTags} label="Tags" update={(tags) => setFieldValue('tags', tags)} errors={errors.tags} touched={touched.tags} />
          {ContextualSubmissionButton('Add session', undefined, dirty && isValid, isLoading)}
        </FormikForm>
      )}
    </Formik>
  )
}

export default AddSessionForm;