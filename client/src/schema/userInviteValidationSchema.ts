import * as Yup from 'yup';

const userInviteValidationSchema = Yup.object().shape({
  usernameOrEmail: Yup.string()
    .required('Required')
});

export default userInviteValidationSchema;