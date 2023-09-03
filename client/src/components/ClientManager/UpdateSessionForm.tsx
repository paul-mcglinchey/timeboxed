import { Formik } from 'formik';
import { sessionValidationSchema } from '../../schema';
import { IContextualFormProps, IGroupClientTagResponse, ISession } from '../../models';
import { useClientService } from '../../hooks';
import { CustomDate, FormikInput, TagInput } from '..';
import { IApiError } from '../../models/error.model';
import { FormikForm } from '../Common/FormikForm';
import { useEffect, useState } from 'react';

interface IUpdateSessionFormProps {
  selectedSession: ISession
  submissionAction?: (() => Promise<void> | void) | undefined
}

const UpdateSessionForm = ({ selectedSession, submissionAction, ContextualSubmissionButton }: IUpdateSessionFormProps & IContextualFormProps) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()
  const [session, setSession] = useState<ISession | null>(null)
  const [groupClientTags, setGroupClientTags] = useState<IGroupClientTagResponse[]>([])

  const { getSessionById, updateSession, getGroupClientTags } = useClientService(setIsLoading, setError)

  const fetchSession = async () => {
    let session = await getSessionById(selectedSession.clientId, selectedSession.id)
    setSession(session)
  }

  const fetchTags = async () => {
    let tags = await getGroupClientTags()
    setGroupClientTags(tags)
  }

  useEffect(() => {
    fetchTags()
    fetchSession()
  }, [selectedSession])

  return (
    <>
      {session && (
        <Formik
          initialValues={{
            title: session.title ?? '',
            description: session.description ?? '',
            sessionDate: (session.sessionDate && session.sessionDate.split('T')[0]) ?? '',
            tags: session.tags || []
          }}
          validationSchema={sessionValidationSchema}
          onSubmit={async (values) => {
            await updateSession(selectedSession.clientId, selectedSession.id, { ...values });
            submissionAction && await submissionAction()
          }}
        >
          {({ values, errors, touched, dirty, isValid, setFieldValue }) => (
            <FormikForm error={error}>
              <FormikInput name="title" label="Title" errors={errors.title} touched={touched.title} />
              <FormikInput as="textarea" name="description" label="Description" errors={errors.description} touched={touched.description} />
              <FormikInput id="sessiondate" type="date" name="sessionDate" label="Session Date" errors={errors.sessionDate} touched={touched.sessionDate} component={CustomDate} />
              <TagInput tags={values.tags} availableTags={groupClientTags} label="Tags" onChange={(tags) => setFieldValue('tags', tags)} />
              {ContextualSubmissionButton('Save changes', undefined, dirty && isValid, isLoading)}
            </FormikForm>
          )}
        </Formik>
      )}
    </>
  )
}

export default UpdateSessionForm;