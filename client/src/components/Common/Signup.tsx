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
          password: ''
        }}
        validationSchema={signupValidationSchema}
        onSubmit={(values) => {
          signup(values);
        }}
      >
        {({ errors, touched }) => (
          <Form>
            <div className="flex flex-col space-y-4">
              <div className="flex-col space-y-2">
                <FormikInput name="email" label="Email" errors={errors.email} touched={touched.email} />
                <FormikInput name="username" label="Username" errors={errors.username} touched={touched.username} />
                <FormikInput name="password" type="password" label="Password" errors={errors.password} touched={touched.password} />
              </div>
              <div className="flex flex-grow justify-end items-center space-x-2">
                {isLoading && (
                  <SpinnerIcon className="w-6 h-6 text-gray-800 dark:text-gray-200" />
                )}
                <Button content="Sign up" />
              </div>
              <div className="flex justify-center">
                <Link to='/login' className='mt-10'>
                  <button type="button" className="font-bold text-gray-500 dark:text-gray-600 dark:hover:text-gray-400 transition-all px-4 py-2">
                    Already a user?
                  </button>
                </Link>
              </div>
            </div>
          </Form>
        )}
      </Formik>
    </PublicWrapper>
  )
}

export default Signup;