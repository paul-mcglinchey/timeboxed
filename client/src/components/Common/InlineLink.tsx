import { Link } from "react-router-dom";
import { combineClassNames } from "../../services";

interface IInlineLinkProps {
  children: any,
  color?: string,
  to: string,
  isActive?: boolean
}

const InlineLink = ({ children, color, to, isActive }: IInlineLinkProps) => {
  return (
    <Link 
      to={to} 
      className={combineClassNames(
        "flex flex-nowrap items-center space-x-2 uppercase text-sm px-2 py-1 font-semibold tracking-wider dark:hover:bg-gray-800 hover:bg-gray-300 rounded-lg", 
        isActive && "dark:bg-gray-800 bg-gray-300",
        color
      )}
    >
      {children}
    </Link>
  )
}

export default InlineLink;