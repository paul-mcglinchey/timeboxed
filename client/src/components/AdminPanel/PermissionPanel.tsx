import { useContext } from 'react'
import { usePermissionService } from '../../hooks'
import { Table } from '../Common'
import { Panel, PermissionTableRow } from '.'
import { MetaInfoContext } from '../../contexts'

const PermissionPanel = () => {

  const { isLoading } = useContext(MetaInfoContext)
  const { permissions } = usePermissionService()

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