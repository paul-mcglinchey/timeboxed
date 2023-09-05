import { combineClassNames } from "../../services"

interface IInputProps {
  id: string | undefined
  type: string
  placeholder: string
  disabled: boolean
}

const Input = ({ id = undefined, type, placeholder, disabled, ...props }: IInputProps) => {
  return (
    <input
      id={id}
      className={combineClassNames(
        "flex-grow dark:text-gray-200 text-gray-900"
      )}
      {...props}
      type={type}
      placeholder={placeholder}
      disabled={disabled}
    />
  )
}

export default Input