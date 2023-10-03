import { IChildrenProps } from "../../models"
import { combineClassNames } from "../../services"

interface ITableRowItemProps extends IChildrenProps {
  hugRight?: boolean
  stack?: boolean
}

const TableRowItem = ({ children, hugRight = false, stack = false }: ITableRowItemProps) => {
  return (
    <td
      className={combineClassNames(
        "whitespace-nowrap dark:text-gray-400 text-gray-900 font-semibold text-md",
        hugRight && "flex items-center"
      )}
    >
      <div className={combineClassNames("flex shrink", hugRight && "justify-end", stack ? "flex-col" : "items-center")}>
        {children}
      </div>
    </td>
  )
}

export default TableRowItem;