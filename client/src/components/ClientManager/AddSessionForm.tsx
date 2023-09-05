import { Formik } from 'formik';
import { sessionValidationSchema } from '../../schema';
import { IClientListResponse, IContextualFormProps, ITag } from '../../models';
import { useClientService } from '../../hooks';
import { IApiError } from '../../models/error.model';
import { FormikForm } from '../Common/FormikForm';
import { useState } from 'react';
import SessionForm from './Sessions/SessionForm';

const currentDate = new Date();
const currentDateAsString = currentDate.toISOString().split('T')[0];

interface IAddSessionFormProps {
  client: IClientListResponse
  submissionAction?: () => Promise<void> | void
}

const AddSessionForm = ({ client, submissionAction, ContextualSubmissionButton }: IAddSessionFormProps & IContextualFormProps) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()

  const { addSession } = useClientService(setIsLoading, setError)

  return (
    <Formik
      initialValues={{
        title: '',
        description: '',
        sessionDate: currentDateAsString || '',
        tags: [] as ITag[]
      }}
      validationSchema={sessionValidationSchema}
      onSubmit={async (values) => {
        await addSession(client.id, { ...values });
        submissionAction && await submissionAction()
      }}
    >
      {(formik) => (
        <FormikForm error={error}>
          <SessionForm formik={formik}/>
          {ContextualSubmissionButton('Add session', undefined, formik.dirty && formik.isValid, isLoading)}
        </FormikForm>
      )}
    </Formik>
  )
}

export default AddSessionForm;