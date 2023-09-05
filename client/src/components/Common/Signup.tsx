import { Link } from 'react-router-dom';
import { Formik } from 'formik';
import { signupValidationSchema } from '../../schema';
import { useAuthService } from '../../hooks';
import { FormikInput, Button } from '.';
import { PublicWrapper, SpinnerIcon } from '.';
import { useState } from 'react';
import { IApiError } from '../../models/error.model';
import { FormikForm } from './FormikForm';
import FormGrouping from './FormGrouping';

const Signup = () => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()

  const { signup } = useAuthService(setIsLoading, setError)

  return (
    <PublicWrapper>
      <Formik
        initialValues={{
          email: '',
          username: '',
          password: '',
          repeatPassword: '',
          accessKey: ''
        }}
        validationSchema={signupValidationSchema}
        onSubmit={(values) => {
          signup(values);
        }}
      >
        {({ errors, touched, dirty, isValid }) => (
          <FormikForm error={error}>
            <FormGrouping>
              <FormikInput name="email" label="Email" errors={errors.email} touched={touched.email} />
              <FormikInput name="username" label="Username" errors={errors.username} touched={touched.username} />
              <FormikInput name="password" type="password" label="Password" errors={errors.password} touched={touched.password} />
              <FormikInput name="repeatPassword" type="password" label="Repeat Password" errors={errors.repeatPassword} touched={touched.repeatPassword} />
              <FormikInput name="accessKey" type="password" label="Alpha Access Key" errors={errors.accessKey} touched={touched.accessKey} helperMessage="This is required for access to the alpha version of the application" />
            </FormGrouping>
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
          </FormikForm>
        )}
      </Formik>
    </PublicWrapper>
  )
}

export default Signup;