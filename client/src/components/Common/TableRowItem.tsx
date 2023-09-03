import { IChildrenProps } from "../../models"
import { combineClassNames } from "../../services"

interface ITableRowItemProps extends IChildrenProps {
  hugRight?: boolean
}

const TableRowItem = ({ children, hugRight = false }: ITableRowItemProps) => {
  return (
    <td
      className="px-6 py-4 whitespace-nowrap dark:text-gray-400 text-gray-900 font-semibold text-md"
    >
      <div className={combineClassNames("flex items-center", hugRight && "justify-end")}>
        {children}
      </div>
    </td>
  )
}

export default TableRowItem;