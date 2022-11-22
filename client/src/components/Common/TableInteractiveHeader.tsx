import { MenuIcon, ArrowDownIcon, ArrowUpIcon } from '@heroicons/react/outline';
import { IChildrenProps, ISortable } from '../../models';
import { SquareIcon } from '.';
import { SortDirection } from '../../enums';

interface IInteractiveHeaderProps extends ISortable, IChildrenProps {
  value: string | undefined
}

const TableInteractiveHeader = ({ 
  sortField, 
  sortDirection, 
  setSortField, 
  setSortDirection, 
  children, 
  value 
}: IInteractiveHeaderProps) => {

  const getIcon = () => {
    if (sortField === value) {
      return sortDirection === "descending" ? ArrowDownIcon : ArrowUpIcon;
    } else {
      return MenuIcon;
    }
  }

  const handleSorting = () => {
    sortField === value ? toggleSortDirection() : setSortField(value);
  }

  const toggleSortDirection = () => {
    setSortDirection(sortDirection === SortDirection.Desc ? SortDirection.Asc : SortDirection.Desc);
  }

  return (
    <button className="flex items-center justify-between space-x-4" onClick={() => handleSorting()}>
      <span className='whitespace-nowrap text-xs font-bold uppercase'>
        {children}
      </span>
      <SquareIcon Icon={getIcon()} size="sm" />
    </button>
  )
}

export default TableInteractiveHeader;