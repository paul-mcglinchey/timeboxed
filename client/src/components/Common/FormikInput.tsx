import { FieldHookConfig, useField } from "formik"
import { Input, InputLabel } from "."
import { combineClassNames } from "../../services"

interface IFormikInputProps {
  label: string
  errors: any,
  touched: any
  disabled?: boolean
  classes?: string
}

const FormikInput = ({
  label,
  errors,
  touched,
  type = "text",
  disabled = false,
  classes,
  ...props
}: IFormikInputProps & FieldHookConfig<string>) => {
  const [field] = useField(props)

  return (
    <div className={combineClassNames("mt-8 relative", classes)}>
      <Input
        type={type}
        placeholder={label}
        disabled={disabled}
        {...field}
      />
      <InputLabel
        htmlFor={field.name}
        label={label}
      />
      <span
        className={combineClassNames(
          "absolute -top-5 right-1 text-sm font-semibold text-rose-500 dark:text-gray-400 transition-all pointer-events-none"
        )}
      >
        {touched && errors}
      </span>
    </div>
  )
}

export default FormikInput;