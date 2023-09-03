import { IChildrenProps } from "../../models"

const TableRowItem = ({ children }: IChildrenProps) => {
  return (
    <td
      className="px-6 py-4 whitespace-nowrap dark:text-gray-400 text-gray-900 font-semibold text-md"
    >
      <div className="flex items-center">
        {children}
      </div>
    </td>
  )
}

export default TableRowItem;