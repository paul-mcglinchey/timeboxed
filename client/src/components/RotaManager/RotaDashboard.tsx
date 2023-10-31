import { memo, useContext, useState } from 'react';
import { Button, Prompter, Toolbar } from '../Common';
import { Application, Permission } from '../../enums';
import { MetaInfoContext } from '../../contexts/MetaInfoContext';
import RotaList from './RotaList';
import RotaModal from './RotaModal';

const RotaDashboard = () => {
  const [addRotaOpen, setAddRotaOpen] = useState(false);

  const { hasPermission } = useContext(MetaInfoContext)

  return (
    <>
      <>
        <Toolbar title="Rotas">
          <>
            {hasPermission(Application.RotaManager, Permission.AddEditDeleteRotas) && (
              <Button buttonType="Toolbar" content="Add rota" action={() => setAddRotaOpen(true)} />
            )}
          </>
        </Toolbar>
        {hasPermission(Application.RotaManager, Permission.ViewRotas) ? (
          <RotaList />
        ) : (
          <Prompter title="You don't have access to view rotas in this group" />
        )}
      </>
      <RotaModal isOpen={addRotaOpen} close={() => setAddRotaOpen(false)} />
    </>
  )
}

export default memo(RotaDashboard)