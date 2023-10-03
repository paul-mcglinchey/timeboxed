import { IChildrenProps } from "../../models";
import { combineClassNames } from "../../services";

interface ITableRowProps extends IChildrenProps {
  action?: () => (Promise<void> | void)
}

const TableRow = ({ action = () => {}, children }: ITableRowProps) => {
  return (
    <tr className={combineClassNames(
      "dark:bg-gray-900 bg-gray-200 dark:border-gray-700 border-gray-400 group hover:bg-gray-300 dark:hover:bg-gray-900"
    )}>
      {children}
    </tr>
  )
}

export default TableRow;