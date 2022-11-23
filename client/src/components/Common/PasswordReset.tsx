import { Formik, Form } from 'formik';
import { FormikInput, Button } from '.';
import PublicWrapper from './PublicWrapper';

const PasswordReset = () => {

  const handleSubmit = (values: { password1: string, password2: string }) => {
    console.log(values)
  }

  return (
    <PublicWrapper>
      <Formik
        initialValues={{
          password1: '',
          password2: ''
        }}
        onSubmit={(values) => {
          handleSubmit(values);
        }}
      >
        {({ errors, touched }) => (
          <Form>
            <div className="flex flex-col space-y-4">
              <div className="flex-col space-y-2">
                <FormikInput name="password1" type="password" label="password1" errors={errors.password1} touched={touched.password1} />
                <FormikInput name="password2" type="password" label="password2" errors={errors.password2} touched={touched.password2} />
              </div>
              <div className="flex justify-between">
                <Button content="Reset password" />
              </div>
            </div>
          </Form>
        )}
      </Formik>
    </PublicWrapper>
  )
}

export default PasswordReset;