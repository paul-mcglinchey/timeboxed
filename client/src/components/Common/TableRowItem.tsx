import { IChildrenProps } from "../../models"

const TableRowItem = ({ children }: IChildrenProps) => {
  return (
    <td
      className="px-6 py-4 text-sm whitespace-nowrap text-gray-400"
    >
      <div className="flex items-center">
        {children}
      </div>
    </td>
  )
}

export default TableRowItem;