import { IChildrenProps } from "../../models"
import { Button } from "../Common"

interface IPanelProps {
  title: string
  subtitle?: string
  HeaderActions?: JSX.Element,
  hideSave?: boolean
}

const Panel = ({ title, subtitle, HeaderActions, hideSave, children }: IPanelProps & IChildrenProps) => {
  return (
    <div className="flex flex-col space-y-4">
      <div>
        <div className="flex justify-between items-center border-b-2 border-gray-600/60 pb-2 mb-4">
          <div>
            <h3 className="text-2xl font-semibold tracking-wide text-blue-500">{title}</h3>
            <span className="text-sm text-gray-400">{subtitle}</span>
          </div>
          {HeaderActions && (
            <div className="flex space-x-4 items-center">
              {HeaderActions}
            </div>
          )}
        </div>
        <div className="flex flex-col space-y-4">
          {children}
        </div>
      </div>
      {!hideSave && (
        <div className="flex justify-end">
          <Button content="Save changes" buttonType="Primary" />
        </div>
      )}
    </div>
  )
}

export default Panel