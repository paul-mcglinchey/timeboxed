import { ChevronUpIcon } from "@heroicons/react/24/solid";
import { ReactNode } from "react";
import { IChildrenProps } from "../../models";
import { combineClassNames } from "../../services";

interface IFormSectionProps extends IChildrenProps {
  title: string
  classes?: string
  expanded?: boolean
  expanderAction?: () => void
  titleActionComponent?: ReactNode
}

const FormSection = ({ children, title, classes, expanded, expanderAction, titleActionComponent }: IFormSectionProps) => {
  return (
    <div className={combineClassNames("flex flex-col mb-4", classes)}>
      {expanderAction ? (
        <button type="button" onClick={() => expanderAction && expanderAction()} className="flex justify-between items-center">
          <FormSectionTitle title={title} />
          <div>
            <ChevronUpIcon className={combineClassNames("w-8 h-8 transform", expanded && "rotate-180")} />
          </div>
        </button>
      ) : (
        <div className="flex justify-between items-center">
          <FormSectionTitle title={title} />
          {titleActionComponent}
        </div>
      )}
      <hr className="border-gray-700 mb-2" />
      {children}
    </div>
  )
}

interface IFormSectionTitleProps {
  title: string
}

const FormSectionTitle = ({ title }: IFormSectionTitleProps) => {
  return (
    <h3 className="text-xl text-color-subheader font-semibold tracking-wide">{title}</h3>
  )
}

export default FormSection;