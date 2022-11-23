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
        "w-full h-10 peer px-1 placeholder-transparent",
        "bg-transparent border-b-2 border-gray-300/20",
        "focus-visible:outline-none",
        "autofill:shadow-fill-gray-700 autofill:text-fill-gray-100"
      )}
      {...props}
      type={type}
      placeholder={placeholder}
      disabled={disabled}
    />
  )
}

export default Input