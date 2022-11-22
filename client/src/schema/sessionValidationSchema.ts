import * as Yup from 'yup';

const sessionValidationSchema = Yup.object().shape({
  title: Yup.string()
    .min(2, 'Too Short!')
    .max(100, 'Too Long!')
    .required('Required'),
  description: Yup.string()
    .max(10000, 'Too Long!'),
  sessionDate: Yup.date()
    .required('Required'),
  tags: Yup.array()
})

export default sessionValidationSchema;