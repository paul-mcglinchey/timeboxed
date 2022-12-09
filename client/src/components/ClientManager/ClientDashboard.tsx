import { useContext, useState } from 'react';
import { Toolbar, Prompter, Button, Modal } from '../Common';
import { AddClientForm, ClientList } from '.';
import { Application, Permission } from '../../enums';
import { MetaInfoContext } from '../../contexts';

const ClientDashboard = () => {
  const [addClientOpen, setAddClientOpen] = useState(false)
  const { hasPermission } = useContext(MetaInfoContext)

  return (
    <>
      <>
        <Toolbar title="Clients">
          <>
            {hasPermission(Application.ClientManager, Permission.AddEditDeleteClients) && (
              <Button buttonType="Toolbar" content="Add client" action={() => setAddClientOpen(true)} />
            )}
          </>
        </Toolbar>
        {hasPermission(Application.ClientManager, Permission.ViewClients) ? (
          <ClientList setAddClientOpen={setAddClientOpen} />
        ) : (
          <Prompter title="You don't have access to view clients in this group" />
        )}
      </>
      <Modal
        title="Add client"
        description="This dialog can be used to create new clients"
        isOpen={addClientOpen}
        close={() => setAddClientOpen(false)}
        allowAddMultiple={true}
      >
        {(ConfirmationButton) => (
          <AddClientForm ContextualSubmissionButton={ConfirmationButton} />
        )}
      </Modal>
    </>
  )
}

export default ClientDashboard;