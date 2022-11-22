import { Link } from 'react-router-dom';
import { Formik, Form } from 'formik';
import { loginValidationSchema } from '../../schema';
import { useAuthService } from '../../hooks';
import { StyledField, Button } from '.';
import { PublicWrapper, SpinnerIcon } from '.';

const Login = () => {

  const { login, isLoading } = useAuthService()

  return (
    <PublicWrapper>
      <Formik
        initialValues={{
          usernameOrEmail: '',
          password: ''
        }}
        validationSchema={loginValidationSchema}
        onSubmit={(values) => {
          login(values)
        }}
      >
        {({ errors, touched }) => (
          <Form>
            <div className="flex flex-col space-y-4">
              <div className="flex-col space-y-2">
                <StyledField name="usernameOrEmail" label="Email or Username" errors={errors.usernameOrEmail} touched={touched.usernameOrEmail} />
                <StyledField name="password" type="password" label="Password" errors={errors.password} touched={touched.password} />
              </div>
              <div className="flex justify-between">
                <Link to='/signup' className="px-4 py-2 font-bold filter drop-shadow-none shadow-none transition-all dark:text-gray-600 dark:hover:text-gray-400">
                  Sign Up
                </Link>
                <div className="flex space-x-2 items-center">
                  {isLoading && (
                    <SpinnerIcon className="w-6 h-6 text-gray-800 dark:text-gray-200" />
                  )}
                  <Button content="Login" />
                </div>
              </div>
              <div className="flex justify-center">
                <Link to='/passwordresetrequest' className="font-bold transition-all px-4 py-2 mt-10 dark:text-gray-600 dark:hover:text-gray-400">
                  Forgot password?
                </Link>
              </div>
            </div>
          </Form>
        )}
      </Formik>
    </PublicWrapper>
  )
}

export default Login;