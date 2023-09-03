import { FC } from "react"
import { IChildrenProps, IProps } from "../../models"
import { combineClassNames } from "../../services"
import SpinnerIcon from "./SpinnerIcon"

interface IDialogButtonProps {
  onClick: () => Promise<void> | void
  disabled?: boolean
  type?: "button" | "submit" | "reset"
  Icon?: FC<IProps> | null | undefined,
  loading?: boolean | undefined
}

const DialogButton = ({ onClick, disabled = false, type = "button", Icon, loading = false, children }: IDialogButtonProps & IChildrenProps) => {
  return (
    <button
      type={type}
      disabled={disabled}
      className={combineClassNames(
        "px-4 py-2 inline-flex justify-center rounded-md border border-transparent transition-all",
        "text-sm font-bold text-slate-600 dark:text-blue-100 bg-blue-400 dark:bg-gray-700",
        "focus:outline-none focus-visible:ring-2 focus-visible:ring-blue-700 focus-visible:ring-offset-2",
        disabled && "opacity-60", "dark:hover:bg-blue-700 hover:bg-blue-600 disabled:pointer-events-none"
      )}
      onClick={() => onClick()}
    >
      {loading && <SpinnerIcon className="h-4 w-4 text-current mr-2 self-center" />}
      {children}
      {Icon && <Icon className="h-4 w-4 text-current ml-2"/>}
    </button>
  )
}

export default DialogButton