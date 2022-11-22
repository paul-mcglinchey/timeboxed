import { IconButtonSize } from "../../enums"

interface IIconButtonProps {
  Icon: any,
  action?: () => void,
  colour?: string,
  className?: string
  buttonSize?: IconButtonSize
}

const SquareIconButton = ({ Icon, action, colour = "text-current", className, buttonSize = IconButtonSize.Medium }: IIconButtonProps) => {

  const getButtonSize = (): string => {
    return `h-${buttonSize - 2} h-${buttonSize - 2} md:h-${buttonSize} md:w-${buttonSize}`
  }

  return (
    <button type="button" onClick={action}>
      <Icon className={`${colour} ${className ? className : ""} ${getButtonSize()} hover:scale-110 transition-transform`} />
    </button>
  )
}

export default SquareIconButton;