import { Formik } from 'formik';
import { sessionValidationSchema } from '../../schema';
import { IContextualFormProps, ISession } from '../../models';
import { useClientService } from '../../hooks';
import { IApiError } from '../../models/error.model';
import { FormikForm } from '../Common/FormikForm';
import { useCallback, useEffect, useState } from 'react';
import SessionForm from './Sessions/SessionForm';

interface IUpdateSessionFormProps {
  selectedSession: ISession
  submissionAction?: (() => Promise<void> | void) | undefined
}

const UpdateSessionForm = ({ selectedSession, submissionAction, ContextualSubmissionButton }: IUpdateSessionFormProps & IContextualFormProps) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()
  const [session, setSession] = useState<ISession | null>(null)

  const { getSessionById, updateSession } = useClientService(setIsLoading, setError)

  const fetchSession = useCallback(async () => {
    let session = await getSessionById(selectedSession.clientId, selectedSession.id)
    setSession(session)
  }, [selectedSession])

  useEffect(() => {
    fetchSession()
  }, [fetchSession, selectedSession])

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
          {(formik) => (
            <FormikForm error={error}>
              <SessionForm formik={formik}/>
              {ContextualSubmissionButton('Save changes', undefined, formik.dirty && formik.isValid, isLoading)}
            </FormikForm>
          )}
        </Formik>
      )}
    </>
  )
}

export default UpdateSessionForm;