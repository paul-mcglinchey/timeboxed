import * as Yup from 'yup';

export const addEmployeeValidationSchema = Yup.object().shape({
  firstName: Yup.string()
    .min(2, 'Too Short!')
    .max(100, 'Too Long!')
    .required('Required'),
  lastName: Yup.string()
    .min(2, 'Too Short!')
    .max(100, 'Too Long!')
    .required('Required'),
  primaryEmailAddress: Yup.string()
    .email('Invalid email')
    .required('Required'),
})

const employeeValidationSchema = Yup.object().shape({
  firstName: Yup.string()
    .min(2, 'Too Short!')
    .max(100, 'Too Long!')
    .required('Required'),
  lastName: Yup.string()
    .min(2, 'Too Short!')
    .max(100, 'Too Long!')
    .required('Required'),
  middleNames: Yup.string()
    .min(2, 'Too Short!')
    .max(100, 'Too Long!'),
  primaryEmailAddress: Yup.string()
    .email('Invalid email')
    .required('Required'),
  birthdate: Yup.date()
    .max(new Date(), 'DOB Cannot be later than the current date')
    .min(new Date(1900, 0), 'Nobody is that old...'),
  startDate: Yup.date()
    .min(new Date(1900, 0), 'Start date cannot be that far in the past'),
  minHours: Yup.number()
    .min(0, 'Minimum hours cannot be less than zero')
    .max(40, 'Minimum hours cannot be more than 40'),
  maxHours: Yup.number()
    .min(0, 'Maximum hours cannot be less than zero')
    .max(100, 'Maximum hours cannot be greater than 100'),
  firstLine: Yup.string()
    .min(2, 'Too Short!')
    .max(100, 'Too Long!'),
  secondLine: Yup.string()
    .min(2, 'Too Short!')
    .max(100, 'Too Long!'),
  thirdLine: Yup.string()
    .min(2, 'Too Short!')
    .max(100, 'Too Long!'),
  city: Yup.string()
    .min(2, 'Too Short!')
    .max(100, 'Too Long!'),
  country: Yup.string()
    .min(2, 'Too Short!')
    .max(100, 'Too Long!'),
  postCode: Yup.string()
    .min(2, 'Too Short!')
    .max(100, 'Too Long!')
    .matches(/^\S+ ?\S+$/, 'Enter a valid Post Code'),
});

export default employeeValidationSchema;