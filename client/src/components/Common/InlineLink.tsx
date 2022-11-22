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
        "flex flex-nowrap items-center space-x-2 uppercase text-sm px-2 py-1 font-medium tracking-wider hover:bg-gray-800 rounded-lg", 
        isActive && "bg-gray-800",
        color  
      )}
    >
      {children}
    </Link>
  )
}

export default InlineLink;