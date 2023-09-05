import { FieldHookConfig, useField } from "formik"
import { InputLabel } from "."
import { combineClassNames } from "../../services"
import { HelperMessage } from "./HelperMessage"

interface IFormikTextAreaProps {
  label: string
  errors: any,
  touched: any
  disabled?: boolean
  classes?: string
  helperMessage?: string
}

const FormikTextArea = ({
  label,
  errors,
  touched,
  disabled = false,
  classes,
  helperMessage,
  ...props
}: IFormikTextAreaProps & FieldHookConfig<string>) => {
  const [field] = useField(props)

  return (
    <div className={combineClassNames("flex flex-grow", classes)}>
      <div className="flex flex-col flex-grow">
        <InputLabel
          htmlFor={field.name}
          label={label}
        >
          {touched && errors && (
            <span
              className={combineClassNames(
                "text-sm font-semibold text-rose-500 transition-all pointer-events-none"
              )}
            >
              {errors}
            </span>
          )}
          {helperMessage && (
            <HelperMessage message={helperMessage} />
          )}
        </InputLabel>
        <textarea
          id={props.id}
          className={combineClassNames(
            "dark:text-gray-200 text-gray-900"
          )}
          placeholder={label}
          disabled={disabled}
          {...field}
        />
      </div>
    </div>
  )
}

export default FormikTextArea;