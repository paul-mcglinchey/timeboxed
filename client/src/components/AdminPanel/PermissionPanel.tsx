import { useContext } from 'react'
import { Table } from '../Common'
import { Panel, PermissionTableRow } from '.'
import { MetaInfoContext } from '../../contexts'

const PermissionPanel = () => {

  const { isLoading, permissions } = useContext(MetaInfoContext)

  const headers = [
    { name: 'Id', value: 'identifier', interactive: true },
    { name: 'Name', value: 'name', interactive: true },
    { name: 'Description', value: 'description', interactive: true },
    { name: 'Language', value: 'language', interactive: true }
  ]

  return (
    <>
      <Panel
        title="Permissions"
        subtitle={`Number of permissions: ${permissions.length}`}
        hideSave
      >
        <Table isLoading={isLoading}>
          <Table.Header headers={headers} />
          <Table.Body>
            {permissions.map((p, i) => (
              <PermissionTableRow key={i} permission={p} />
            ))}
          </Table.Body>
        </Table>
      </Panel>
    </>
  )
}

export default PermissionPanel