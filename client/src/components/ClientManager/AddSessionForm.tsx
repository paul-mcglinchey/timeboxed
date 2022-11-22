import { useState } from 'react';
import { Formik, Form } from 'formik';
import { sessionValidationSchema } from '../../schema';
import { IClient, IContextualFormProps, ITag } from '../../models';
import { useClientService } from '../../hooks';
import { CustomDate, StyledField, StyledTagField } from '..';

const currentDate = new Date();
const currentDateAsString = currentDate.toISOString().split('T')[0];

interface IAddSessionProps {
  client: IClient
}

const AddSessionForm = ({ client, ContextualSubmissionButton }: IAddSessionProps & IContextualFormProps) => {

  const [tags, setTags] = useState<ITag[]>([])

  const { addSession } = useClientService()

  return (
    <div className="flex flex-1">
      <Formik
        initialValues={{
          title: '',
          description: '',
          sessionDate: currentDateAsString || '',
        }}
        validationSchema={sessionValidationSchema}
        onSubmit={(values, { resetForm }) => {
          addSession(client.id, { ...values, tags: tags });
          resetForm();
        }}
      >
        {({ errors, touched, dirty, isValid }) => (
          <Form className="flex flex-grow flex-col">
            <div className="flex flex-col space-y-3 content-end">
              <div className="flex flex-col md:flex-row md:space-x-2 space-x-0 space-y-1 md:space-y-0"></div>
              <StyledField name="title" label="Title" errors={errors.title} touched={touched.title} />
              <StyledField as="textarea" name="description" label="Description" errors={errors.description} touched={touched.description} />
              <div className="flex flex-col sm:flex-row item-center space-x-0 space-y-2 sm:space-x-4 sm:space-y-0">
                <StyledField type="date" name="sessionDate" label="Session Date" errors={errors.sessionDate} touched={touched.sessionDate} component={CustomDate} />
                <StyledTagField name="tags" label="Tags" tags={tags} setTags={setTags} errors={undefined} touched={undefined} />
              </div>
            </div>
            {ContextualSubmissionButton('Add session', undefined, dirty && isValid)}
          </Form>
        )}
      </Formik>
    </div >
  )
}

export default AddSessionForm;