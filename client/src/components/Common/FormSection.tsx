import { ChevronUpIcon } from "@heroicons/react/solid";
import { IChildrenProps } from "../../models";
import { combineClassNames } from "../../services";
import { Switch } from ".";

interface IFormSectionProps extends IChildrenProps {
  title: string
  classes?: string
  state?: boolean
  setState?: (state: boolean) => void
  showExpander?: boolean
  expanded?: boolean
  expanderAction?: () => void
}

const FormSection = ({ children, title, classes, state, setState, showExpander, expanded, expanderAction }: IFormSectionProps) => {
  return (
    <div className={combineClassNames("flex flex-col", classes)}>
      {showExpander && expanderAction ? (
        <button type="button" onClick={() => expanderAction()} className="flex justify-between items-center pb-2">
          <div>
            <h3 className="text-2xl font-semibold tracking-wide">{title}</h3>
          </div>
          <div>
            {typeof state === "boolean" && typeof setState === "function" && (
              <Switch enabled={state} setEnabled={() => setState(!state)} description="" />
            )}
            <ChevronUpIcon className={combineClassNames("w-8 h-8 transform", expanded && "rotate-180")} />
          </div>
        </button>
      ) : (
        <div className="flex justify-between items-center pb-2">
          <h3 className="text-2xl font-semibold tracking-wide">{title}</h3>
          {typeof state === "boolean" && typeof setState === "function" && (
            <Switch enabled={state} setEnabled={() => setState(!state)} description="" />
          )}
        </div>
      )}
      <hr className="border-gray-700 mb-2" />
      {children}
    </div>
  )
}

export default FormSection;