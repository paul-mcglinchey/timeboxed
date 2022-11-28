import * as Yup from 'yup';

const signupValidationSchema = Yup.object().shape({
  email: Yup.string()
    .min(8, 'Too Short!')
    .max(100, 'Too Long!')
    .required('Required'),
  username: Yup.string()
    .min(6, 'Too Short!')
    .max(50, 'Too Long!')
    .required('Required'),
  password: Yup.string()
    .min(8, 'Minimum 8 Characters in Length')
    .max(100, 'Too Long')
    .required('Required'),
  repeatPassword: Yup.string()
    .required('Required')
    .oneOf([Yup.ref('password')], 'Passwords don\'t match')
});

export default signupValidationSchema;