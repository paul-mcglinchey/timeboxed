import { IChildrenProps } from "../../models"

interface ISubPanelProps {
  Title?: JSX.Element
  Subtitle?: JSX.Element,
  HeaderActions?: JSX.Element
}

const SubPanel = ({ Title, Subtitle, HeaderActions, children }: ISubPanelProps & IChildrenProps) => {
  return (
    <div className="bg-gray-800 rounded p-4 flex flex-col space-y-4">
      {Title && (
        <div className="flex justify-between items-center pb-2 border-b border-gray-200/20">
          <div className="flex flex-grow flex-col space-y-1">
            {Title}
            {Subtitle && Subtitle}
          </div>
          {HeaderActions && HeaderActions}
        </div>
      )}
      {children}
    </div>
  )
}

export default SubPanel