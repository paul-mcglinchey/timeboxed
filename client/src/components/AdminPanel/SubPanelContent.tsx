import { IChildrenProps } from "../../models"

interface ISubPanelContentProps {
  SubPanelActions?: JSX.Element
}

const SubPanelContent = ({ children, SubPanelActions }: ISubPanelContentProps & IChildrenProps) => {
  return (
    <div className="space-y-4">
      <div className="flex flex-col space-y-2">
        {children}
      </div>
      {SubPanelActions && SubPanelActions}
    </div>
  )
}

export default SubPanelContent