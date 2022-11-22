import { combineClassNames } from "../../services";

interface IInlineButtonProps {
  children: any,
  color?: string,
  action: () => void
}

const InlineButton = ({ children, action, color }: IInlineButtonProps) => {
  return (
    <button
      type="button"
      onClick={action} 
      className={combineClassNames(
        "flex flex-nowrap items-center space-x-2 uppercase text-sm px-2 py-1 font-medium tracking-wider hover:bg-gray-800 rounded-lg", 
        color
      )}
    >
      {children}
    </button>
  )
}

export default InlineButton;