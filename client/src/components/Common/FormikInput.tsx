import { format } from "date-fns"
import { FieldHookConfig, useField, useFormikContext } from "formik"
import { useEffect } from "react"
import { Input, InputLabel } from "."
import { combineClassNames } from "../../services"
import { HelperMessage } from "./HelperMessage"

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

  const { setFieldValue } = useFormikContext()

  const handleDateInputKeyDown = (e: KeyboardEvent) => {
    if (e.key === 'T' || e.key === 't') {
      setFieldValue(field.name, format(new Date(), 'yyy-MM-dd'))
    }
  }

  useEffect(() => {
    if (type === "date" && props.id) {
      let dateInput = document.getElementById(props.id)
      dateInput?.addEventListener('keydown', handleDateInputKeyDown)
    }

    return () => {
      if (props.id) {
        document.getElementById(props.id)?.removeEventListener('keydown', handleDateInputKeyDown)
      }
    }
  }, [handleDateInputKeyDown, props, type])

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
                "text-sm font-semibold text-rose-500 pointer-events-none"
              )}
            >
              {errors}
            </span>
          )}
          {helperMessage && (
            <HelperMessage message={helperMessage} />
          )}
        </InputLabel>
        <Input
          id={props.id}
          type={type}
          placeholder={label}
          disabled={disabled}
          {...field}
        />
      </div>
    </div>
  )
}

export default FormikInput;