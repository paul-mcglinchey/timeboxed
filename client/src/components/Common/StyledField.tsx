import { FieldHookConfig, useField } from "formik"
import { combineClassNames } from "../../services"

interface IFieldProps {
  label: string
  compact?: boolean
  errors: any,
  touched: any
  noAutocomplete?: boolean
  disabled?: boolean
  classes?: string
}

const StyledField = ({ label, errors, touched, placeholder, type = "text", compact = false, noAutocomplete, disabled = false, classes, ...props }: IFieldProps & FieldHookConfig<string>) => {
  const [field] = useField(props)

  return (
    <div className={combineClassNames("w-full relative text-sm sm:text-base")}>
      {!compact && (
        <label 
          className={combineClassNames(
            "uppercase text-sm font-bold",
            "text-gray-900 dark:text-gray-400"
          )}
          htmlFor={field.name}
        >{label}</label>
      )}
      <input className={combineClassNames(
        "rounded w-full px-3 py-2 sm:px-4 mt-1",
        "bg-gray-200 dark:bg-gray-900",
        "autofill:shadow-fill-blue-200"
      )}
        {...field}
        type={type}
        autoComplete={noAutocomplete ? "off" : "on"}
        placeholder={compact ? (placeholder || label) : placeholder}
        disabled={disabled}
      />
    </div>
  )
}

export default StyledField;