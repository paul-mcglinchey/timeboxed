import { IChildrenProps } from "../../models";
import { combineClassNames } from "../../services";

interface ITableHeaderProps extends IChildrenProps {
  hugRight?: boolean
}

const TableHeader = ({ children, hugRight = false }: ITableHeaderProps) => {

  return (
    <th
      scope="col"
      className={combineClassNames("py-3 px-6 text-xs tracking-wider dark:text-gray-400 text-gray-800 uppercase", hugRight ? "text-right" : "text-left")}
    >
      {children}
    </th>
  )
}

export default TableHeader;