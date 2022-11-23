import { Dispatch, Fragment, SetStateAction } from 'react';
import { UserAddIcon } from '@heroicons/react/solid';
import { IClientListResponse, IFilterableField } from '../../models';
import { useAuthService, useClientService } from '../../hooks';
import { Paginator, Table, Prompter, SearchBar } from '../Common';
import { ClientTableRow } from '.';
import { Application, Permission } from '../../enums';
import { clientTableHeaders } from '../../config';

interface IClientListProps {
  setAddClientOpen: Dispatch<SetStateAction<boolean>>
}

const ClientList = ({ setAddClientOpen }: IClientListProps) => {

  const { clients, count, filter, setFilter, sortField, setSortField, sortDirection, setSortDirection, isLoading, pageNumber, setPageNumber, pageSize, setPageSize } = useClientService()
  const { hasPermission } = useAuthService()

  const filterApplied = (): boolean => filter.value !== null

  const filterableFields: IFilterableField[] = [
    { name: 'name', label: 'Name' },
    { name: 'email', label: 'Email' }
  ]

  return (
    <div className="rounded-lg flex flex-col space-y-0 pb-2">
      {(count > 0 || filterApplied()) ? (
        <Fragment>
          <div className="flex flex-col flex-grow space-y-4">
            <SearchBar
              filter={filter}
              setFilter={setFilter}
              initialFilterField={filterableFields[0]!}
              filterableFields={filterableFields}
            />
            <Table isLoading={isLoading}>
              <Table.SortableHeader headers={clientTableHeaders} sortField={sortField} setSortField={setSortField} sortDirection={sortDirection} setSortDirection={setSortDirection} />
              <Table.Body>
                {clients.map((c: IClientListResponse, index: number) => (
                  <ClientTableRow client={c} key={index} />
                ))}
              </Table.Body>
            </Table>
          </div>
          <Paginator pageNumber={pageNumber} pageSize={pageSize} setPageNumber={setPageNumber} setPageSize={setPageSize} totalItems={count} itemType="client" />
        </Fragment>
      ) : (
        !isLoading && (
          hasPermission(Application.ClientManager, Permission.AddEditDeleteClients) ? (
            <Prompter title="Add a client to get started" Icon={UserAddIcon} action={() => setAddClientOpen(true)} />
          ) : (
            <Prompter title="There are no clients added to this group yet" />
          )
        )
      )}
    </div>
  )
}

export default ClientList;