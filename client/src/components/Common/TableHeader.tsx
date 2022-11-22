import { IChildrenProps } from "../../models";

const TableHeader = ({ children }: IChildrenProps) => {

  return (
    <th
      scope="col"
      className="py-3 px-6 text-xs font-medium tracking-wider text-left text-gray-400 uppercase"
    >
      {children}
    </th>
  )
}

export default TableHeader;