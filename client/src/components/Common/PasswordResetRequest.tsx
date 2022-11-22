import { Link } from 'react-router-dom';
import { Formik, Form } from 'formik';
import { StyledField, Button } from '.';
import PublicWrapper from './PublicWrapper';

const PasswordResetRequest = () => {

  const handleSubmit = (values: { email: string }) => {
    console.log(values)
  }

  return (
    <PublicWrapper>
      <Formik
        initialValues={{
          email: ''
        }}
        onSubmit={(values) => {
          handleSubmit(values);
        }}
      >
        {({ errors, touched }) => (
          <Form>
            <div className="flex flex-col space-y-4">
              <div className="flex-col space-y-2">
                <StyledField name="email" label="Email" errors={errors.email} touched={touched.email} />
              </div>
              <div className="flex justify-between">
                <Link to='/login'>
                  <button type="button" className="px-4 py-2 font-bold dark:text-gray-600 dark:hover:text-gray-400 transition-all">
                    Cancel
                  </button>
                </Link>
                <Button content="Request password reset" />
              </div>
            </div>
          </Form>
        )}
      </Formik>
    </PublicWrapper>
  )
}

export default PasswordResetRequest;