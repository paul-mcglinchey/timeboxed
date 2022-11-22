import * as Yup from 'yup';

const loginValidationSchema = Yup.object().shape({
  usernameOrEmail: Yup.string()
    .required('Required'),
  password: Yup.string()
    .required('Required')
});

export default loginValidationSchema;