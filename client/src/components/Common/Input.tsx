import { combineClassNames } from "../../services"

interface IInputProps {
  type: string
  placeholder: string
  disabled: boolean
}

const Input = ({ type, placeholder, disabled, ...props }: IInputProps) => {
  return (
    <input
      className={combineClassNames(
        "peer placeholder-transparent"
      )}
      {...props}
      type={type}
      placeholder={placeholder}
      disabled={disabled}
    />
  )
}

export default Input