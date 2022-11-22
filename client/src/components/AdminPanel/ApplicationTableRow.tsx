import { IApplication } from "../../models"
import { TableRow, TableRowItem } from "../Common"

interface IApplicationTableRowProps {
  application: IApplication
}

const ApplicationTableRow = ({ application }: IApplicationTableRowProps) => {
  return (
    <TableRow>
      <TableRowItem>
        <span className="text-sm font-medium text-white">{application.id}</span>
      </TableRowItem>
      <TableRowItem>
        <span style={{ borderColor: application.colour }} className={`px-2 py-1 rounded-lg border`}>{application.name}</span>
      </TableRowItem>
      <TableRowItem>
        <span className="max-w-xs overflow-hidden text-ellipsis">{application.description}</span>
      </TableRowItem>
      <TableRowItem>
        <div className="flex items-center space-x-4 min-w-40">
          <span>{application.url}</span>
        </div>
      </TableRowItem>
    </TableRow>
  )
}

export default ApplicationTableRow