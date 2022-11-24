import { combineClassNames } from "../../services"

interface IInputLabelProps {
  htmlFor: string
  label: string
  visibilityConditions?: boolean[]
}

const InputLabel = ({ htmlFor, label, visibilityConditions = [] }: IInputLabelProps) => {
  return (
    <label
      htmlFor={htmlFor}
      className={combineClassNames(
        "absolute -top-5 left-1 text-sm font-semibold dark:text-gray-400 transition-all pointer-events-none",
        visibilityConditions.every(vc => vc) && "peer-placeholder-shown:text-gray-900/30 dark:peer-placeholder-shown:text-gray-100/60 peer-placeholder-shown:text-base peer-placeholder-shown:top-2 peer-placeholder-shown:left-3.5"
      )}
    >
      {label}
    </label>
  )
}

export default InputLabel