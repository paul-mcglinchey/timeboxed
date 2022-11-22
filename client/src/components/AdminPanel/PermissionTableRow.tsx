import { IPermission } from "../../models"
import { TableRow, TableRowItem } from "../Common"

interface IPermissionTableRowProps {
  permission: IPermission
}

const PermissionTableRow = ({ permission }: IPermissionTableRowProps) => {

  return (
    <TableRow>
      <TableRowItem>
        <div className="flex flex-col">
          <div className="text-sm font-medium text-white">{permission.id}</div>
        </div>
      </TableRowItem>
      <TableRowItem>
        <div className="flex items-center space-x-4 min-w-40">
          <span>{permission.name}</span>
        </div>
      </TableRowItem>
      <TableRowItem>
        <div className="flex items-center space-x-4 min-w-40">
          <span>{permission.description}</span>
        </div>
      </TableRowItem>
      <TableRowItem>
        <div className="flex items-center space-x-4 min-w-40">
          <span>{permission.language}</span>
        </div>
      </TableRowItem>
    </TableRow>
  )
}

export default PermissionTableRow