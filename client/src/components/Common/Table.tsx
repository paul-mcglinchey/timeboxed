import { IChildrenProps, ISortable, ITable, ITableHeader } from "../../models"
import { SpinnerIcon, TableHeader, TableInteractiveHeader } from "."
import { combineClassNames } from "../../services"

interface ITableProps extends ITable, IChildrenProps { }

const Table = ({
  isLoading,
  children,
  compact = false
}: ITableProps) => {
  return (
    <div className={combineClassNames(
      "overflow-x-auto rounded-md relative",
      compact ? "[&_td]:px-1 [&_td]:py-2 [&_td:nth-child(1)]:pl-5 [&_td:last-child]:pr-5" : "[&_td]:px-6 [&_td]:py-4",
    )}>
      <table className="min-w-full">
        {children}
      </table>
      {isLoading && (
        <div className="absolute right-0 top-0 rounded-md p-2 justify-center">
          <SpinnerIcon className="text-white h-6 w-6" />
        </div>
      )}
    </div>
  )
}

Table.Header = ({ headers }: ITableHeader) => {
  return (
    <thead className="dark:bg-gray-800 bg-gray-300">
      <tr>
        {headers.map((h, i) => (
          <TableHeader key={i} hugRight={h.hugRight}>
            {h.name}
          </TableHeader>
        ))}
      </tr>
    </thead>
  )
}

Table.SortableHeader = ({ headers, sortField, sortDirection, setSortField, setSortDirection }: ITableHeader & ISortable) => {
  return (
    <thead className="dark:bg-gray-800 bg-gray-300">
      <tr>
        {headers.map((h, i) => (
          <TableHeader key={i} hugRight={h.hugRight}>
            {h.interactive ? (
              <TableInteractiveHeader sortField={sortField} sortDirection={sortDirection} setSortField={setSortField} setSortDirection={setSortDirection} key={i} value={h.value}>
                {h.name}
              </TableInteractiveHeader>
            ) : (
              h.name
            )}
          </TableHeader>
        ))}
      </tr>
    </thead>
  )
}

Table.Body = ({ children }: IChildrenProps) => {
  return (
    <tbody className="divide-y dark:divide-gray-700 divide-gray-300">
      {children}
    </tbody>
  )
}

export default Table;