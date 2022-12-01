import { QuestionMarkCircleIcon } from "@heroicons/react/solid"
import { FieldHookConfig, useField } from "formik"
import { Input, InputLabel } from "."
import { combineClassNames } from "../../services"

interface IFormikInputProps {
  label: string
  errors: any,
  touched: any
  disabled?: boolean
  classes?: string
  helperMessage?: string
}

const FormikInput = ({
  label,
  errors,
  touched,
  type = "text",
  disabled = false,
  classes,
  helperMessage,
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

      {touched && errors && (
        <span
          className={combineClassNames(
            "absolute -top-5 right-1 text-sm font-semibold text-rose-500 transition-all pointer-events-none"
          )}
        >
          {errors}
        </span>
      )}

      {helperMessage && (
        <div className="absolute right-2 top-2.5">
          <QuestionMarkCircleIcon className="w-6 h-6 text-blue-500 peer hover:opacity-80 transition-all" />
          <div className="absolute w-48 bg-gray-300 dark:bg-slate-500 text-xs font-semibold p-2 shadow-sm rounded-md origin-top-right right-0 top-7 hidden peer-hover:block">
            {helperMessage}
          </div>
        </div>
      )}
    </div>
  )
}

export default FormikInput;