import { IChildrenProps } from "../../models";

const TableRow = ({ children }: IChildrenProps) => {
  return (
    <tr className="dark:bg-gray-900 bg-gray-200 dark:border-gray-700 border-gray-400">
      {children}
    </tr>
  )
}

export default TableRow;