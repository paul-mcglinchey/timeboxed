import { IChildrenProps } from "../../models"
import { combineClassNames } from "../../services"

interface IInputLabelProps extends IChildrenProps {
  htmlFor: string
  label: string
}

const InputLabel = ({ htmlFor, label, children }: IInputLabelProps) => {
  return (
    <div className="inline-flex justify-between items-center">
      <label
        htmlFor={htmlFor}
        className={combineClassNames(
          "text-xs dark:text-gray-400 pointer-events-none"
        )}
      >
        {label}
      </label>
      <div className="flex gap-2 items-center">
        {children}
      </div>
    </div>
  )
}

export default InputLabel