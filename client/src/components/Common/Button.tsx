import { combineClassNames } from '../../services';
import { INotification, IProps } from '../../models';
import { FC } from 'react';

export interface IButtonProps {
  status?: INotification[],
  content?: string,
  buttonType?: "Primary" | "Secondary" | "Tertiary" | "Cancel" | "Confirm" | "Toolbar",
  type?: "button" | "submit" | "reset",
  action?: () => void,
  hideText?: boolean,
  Icon?: FC<IProps> | null | undefined,
  iconSide?: "left" | "right"
  XL?: boolean
  disabled?: boolean
}

const getButtonClasses = (buttonType: string): string => {
  switch (buttonType) {
    case "Primary":
      return "border-2 border-blue-600 text-blue-600 bg-transparent hover:text-gray-900 hover:bg-blue-600 hover:border-transparent focus:text-gray-900 focus:bg-blue-600"
    case "Secondary":
      return "text-blue-500 bg-transparent border border-transparent hover:text-gray-300 hover:border-blue-500 focus:text-gray-300 focus:bg-blue-500"
    case "Tertiary":
      return "border border-transparent text-blue-500 bg-transparent hover:text-blue-400"
    case "Cancel":
      return "border border-transparent text-red-500 bg-transparent hover:border-red-700"
    case "Confirm":
      return "text-green-500 bg-transparent hover:text-green-600"
    case "Toolbar":
      return combineClassNames(
        "h-10 w-full relative py-2 px-3 text-right font-semibold",
        "text-gray-200 bg-blue-500/70 dark:bg-gray-800 rounded-lg shadow-md focus:outline-none focus-visible:outline-blue-500 focus-visible:outline-1",
        "focus:outline-none focus-visible:outline-blue-500 focus-visible:outline-1",
        "hover:-translate-y-0.5 dark:hover:bg-blue-500 dark:hover:text-gray-900 transition-all"
      )
    default:
      return "border border-gray-300 bg-transparent hover:text-gray-900 hover:bg-gray-300 focus:text-gray-900 focus:bg-gray-300"
  }
}

const Button = ({
  content,
  type = "submit",
  buttonType = "Tertiary",
  action = () => { },
  hideText,
  Icon,
  iconSide = "right",
  XL = false,
  disabled = false
}: IButtonProps) => {
  return (
    <button className={
      combineClassNames(
        getButtonClasses(buttonType),
        content ? "px-3" : "px-1",
        "py-1 transition-all font-bold rounded-md flex items-center justify-center tracking-wider",
        XL && "text-xl",
        "disabled:pointer-events-none disabled:text-opacity-50 select-none"
      )}
      onClick={action}
      disabled={disabled}
      type={type}
    >
      {Icon && (
        <div className={`self-center ${content && (iconSide === "left" ? "order-first mr-2" : "order-last ml-2")}`}>
          {<Icon className="w-5 h-5" />}
        </div>
      )}
      {!hideText && (
        <div>
          {content}
        </div>
      )}
    </button>
  )
}

export default Button;