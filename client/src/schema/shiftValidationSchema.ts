import * as Yup from 'yup';

const shiftValidationSchema = Yup.object().shape({
  startHour: Yup.number()
    .when('endHour', { is: (endHour: string) => endHour.length > 2, then: (schema) => schema.required('Required') })
    .integer('Invalid hour')
    .max(23, 'Invalid hour')
    .min(0, 'Invalid hour'),
  endHour: Yup.number()
    .integer('Invalid hour')
    .max(23, 'Invalid hour')
    .min(0, 'Invalid hour')
});

export default shiftValidationSchema;