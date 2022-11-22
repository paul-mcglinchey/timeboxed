import * as Yup from 'yup';

const rotaValidationSchema = Yup.object().shape({
  name: Yup.string()
    .required('Required')
    .min(2, 'Too short!')
    .max(50, 'Too long!'),
  closingHour: Yup.number()
    .integer('Invalid hour')
    .required('Required')
    .max(23, 'Invalid hour')
    .min(0, 'Invalid hour')
});

export default rotaValidationSchema;