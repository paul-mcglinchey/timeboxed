import { Link } from 'react-router-dom';
import { Formik, Form } from 'formik';
import { signupValidationSchema } from '../../schema';
import { useAuthService } from '../../hooks';
import { FormikInput, Button } from '.';
import { PublicWrapper, SpinnerIcon } from '.';

const Signup = () => {

  const { isLoading } = useAuthService()
  const { signup } = useAuthService();

  return (
    <PublicWrapper>
      <Formik
        initialValues={{
          email: '',
          username: '',
          password: '',
          repeatPassword: ''
        }}
        validationSchema={signupValidationSchema}
        onSubmit={(values) => {
          signup(values);
        }}
      >
        {({ errors, touched, dirty, isValid }) => (
          <Form>
            <div className="mb-4">
              <FormikInput name="email" label="Email" errors={errors.email} touched={touched.email} />
              <FormikInput name="username" label="Username" errors={errors.username} touched={touched.username} />
              <FormikInput name="password" type="password" label="Password" errors={errors.password} touched={touched.password} />
              <FormikInput name="repeatPassword" type="password" label="Repeat Password" errors={errors.repeatPassword} touched={touched.repeatPassword} />
            </div>
            <div className="flex flex-grow justify-end items-center space-x-2">
              {isLoading && (
                <SpinnerIcon className="w-6 h-6 text-gray-800 dark:text-gray-200" />
              )}
              <Button disabled={!dirty || (dirty && !isValid)} content="Sign up" />
            </div>
            <div className="flex justify-center">
              <Link to='/login' className='mt-10'>
                <button type="button" className="font-bold text-gray-500 dark:text-gray-600 dark:hover:text-gray-400 transition-all px-4 py-2">
                  Already a user?
                </button>
              </Link>
            </div>
          </Form>
        )}
      </Formik>
    </PublicWrapper>
  )
}

export default Signup;