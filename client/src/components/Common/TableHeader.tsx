import { IChildrenProps } from "../../models";

const TableHeader = ({ children }: IChildrenProps) => {

  return (
    <th
      scope="col"
      className="py-3 px-6 text-xs tracking-wider text-left dark:text-gray-400 text-gray-800 uppercase"
    >
      {children}
    </th>
  )
}

export default TableHeader;