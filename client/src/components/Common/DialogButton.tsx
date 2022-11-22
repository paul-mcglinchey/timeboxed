import { IChildrenProps } from "../../models"
import { combineClassNames } from "../../services"

interface IDialogButtonProps {
  actions: (() => void)[]
  disabled?: boolean
  type?: "button" | "submit" | "reset"
}

const DialogButton = ({ actions, disabled = false, type = "button", children }: IDialogButtonProps & IChildrenProps) => {
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
      onClick={() => actions.forEach(a => a())}
    >
      {children}
    </button>
  )
}

export default DialogButton