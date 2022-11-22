import { memo, useState } from 'react';
import { useAuthService } from '../../hooks';
import { Button, Prompter, Toolbar } from '../Common';
import { RotaModal, RotaList } from '.';
import { Application, Permission } from '../../enums';

const RotaDashboard = () => {
  const [addRotaOpen, setAddRotaOpen] = useState(false);

  const { hasPermission } = useAuthService()

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