import { useContext, useState } from 'react';
import { TableIcon } from '@heroicons/react/solid';
import { IRota } from '../../models';
import { Prompter, SpinnerLoader, Table } from '../Common';
import { RotaModal, RotaTableRow } from '.';
import { Application, Permission } from '../../enums';
import { rotaTableHeaders } from '../../config';
import { AuthContext, RotaContext } from '../../contexts';

const RotaList = () => {
  
  const [addRotaOpen, setAddRotaOpen] = useState(false);
  
  const { rotas, isLoading, sortField, sortDirection, setSortField, setSortDirection } = useContext(RotaContext);
  const { hasPermission } = useContext(AuthContext)

  return (
    <>
      <div className="rounded-lg flex flex-col space-y-0 pb-2 min-h-96">
        {rotas.length > 0 ? (
          <>
            <div className="flex flex-col flex-grow space-y-4">
              <Table
                isLoading={isLoading}
              >
                <Table.SortableHeader headers={rotaTableHeaders} sortField={sortField} sortDirection={sortDirection} setSortField={setSortField} setSortDirection={setSortDirection} />
                <Table.Body>
                  {rotas.map((r: IRota, index: number) => (
                    <RotaTableRow rota={r} key={index} />
                  ))}
                </Table.Body>
              </Table>
            </div>
          </>
        ) : (
          isLoading ? (
            <SpinnerLoader />
          ) : (
            hasPermission(Application.RotaManager, Permission.AddEditDeleteRotas) ? (
              <Prompter title="Add a rota to get started" Icon={TableIcon} action={() => setAddRotaOpen(true)} />
            ) : (
              <Prompter title="There are no rotas added to this group yet" />
            )
          )
        )}
      </div>
      <RotaModal isOpen={addRotaOpen} close={() => setAddRotaOpen(false)} />
    </>
  )
}

export default RotaList