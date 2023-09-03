import * as Yup from 'yup';

const sessionValidationSchema = Yup.object().shape({
  title: Yup.string()
    .min(2, 'Too Short!')
    .max(100, 'Too Long!')
    .required('Required'),
  description: Yup.string()
    .max(10000, 'Too Long!'),
  sessionDate: Yup.date().min('01-01-1970'),
  tags: Yup.array().max(5, 'A maximum of 5 tags are allowed')
})

export default sessionValidationSchema;