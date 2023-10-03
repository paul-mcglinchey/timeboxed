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
      {...props}
      type={type}
      placeholder={placeholder}
      disabled={disabled}
    />
  )
}

export default Input