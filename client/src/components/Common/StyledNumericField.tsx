import { Field } from "formik";
import { combineClassNames } from "../../services";

interface INumericFieldProps {
  name: string,
  label?: string,
  errors: any,
  touched: any,
  autoComplete?: string,
  maxLength?: string,
  component?: React.ReactNode,
  type?: string,
  as?: string
}

const StyledNumericField = ({ name, errors, touched, autoComplete, component, type, as, maxLength = "2" }: INumericFieldProps) => {
  return (
    <Field
      className={combineClassNames(
        `w-10 h-10 focus:outline-none text-gray-200 bg-gray-800 rounded text-center font-semibold tracking-wider text-xl uppercase leading-loose`,
        errors && touched && 'border border-red-600'
      )}
      name={name}
      autoComplete={autoComplete}
      component={component}
      type={type}
      maxLength={maxLength}
      as={as}
    />
  )
}

export default StyledNumericField;