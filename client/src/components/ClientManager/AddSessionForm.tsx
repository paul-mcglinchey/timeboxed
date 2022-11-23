import { Formik, Form } from 'formik';
import { sessionValidationSchema } from '../../schema';
import { IClientListResponse, IContextualFormProps } from '../../models';
import { useClientService } from '../../hooks';
import { CustomDate, FormikInput, TagInput } from '..';

const currentDate = new Date();
const currentDateAsString = currentDate.toISOString().split('T')[0];

interface IAddSessionProps {
  client: IClientListResponse
}

const AddSessionForm = ({ client, ContextualSubmissionButton }: IAddSessionProps & IContextualFormProps) => {

  const { addSession } = useClientService()

  return (
    <Formik
      initialValues={{
        title: '',
        description: '',
        sessionDate: currentDateAsString || '',
        tags: []
      }}
      validationSchema={sessionValidationSchema}
      onSubmit={(values, { resetForm }) => {
        addSession(client.id, { ...values });
        resetForm();
      }}
    >
      {({ values, errors, touched, dirty, isValid, setFieldValue }) => (
        <Form>
          <FormikInput name="title" label="Title" errors={errors.title} touched={touched.title} />
          <FormikInput as="textarea" name="description" label="Description" errors={errors.description} touched={touched.description} />
          <FormikInput type="date" name="sessionDate" label="Session Date" errors={errors.sessionDate} touched={touched.sessionDate} component={CustomDate} />
          <TagInput tags={values.tags} label="Tags" onChange={(tags) => setFieldValue('tags', tags)} />
          {ContextualSubmissionButton('Add session', undefined, dirty && isValid)}
        </Form>
      )}
    </Formik>
  )
}

export default AddSessionForm;