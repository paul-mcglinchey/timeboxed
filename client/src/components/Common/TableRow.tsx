import { IChildrenProps } from "../../models";

const TableRow = ({ children }: IChildrenProps) => {
  return (
    <tr className="bg-gray-900 border-gray-700">
      {children}
    </tr>
  )
}

export default TableRow;