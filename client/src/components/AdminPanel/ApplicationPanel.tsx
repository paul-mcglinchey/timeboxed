import { useContext } from 'react'
import { useApplicationService } from '../../hooks'
import { Table } from '../Common'
import { ApplicationTableRow } from '.'
import { MetaInfoContext } from '../../contexts/MetaInfoContext'
import Panel from './Panel'

const ApplicationPanel = () => {

  const { isLoading } = useContext(MetaInfoContext)
  const { applications = [] } = useApplicationService()

  const headers = [
    { name: 'Id' },
    { name: 'Name' },
    { name: 'Description' },
    { name: 'URL/Route' }
  ]

  return (
    <>
      {!isLoading && (
        <Panel
          title="Applications"
          subtitle={`Number of applications: ${applications.length}`}
          hideSave
        >
          <Table isLoading={isLoading}>
            <Table.Header headers={headers} />
            <Table.Body>
              {applications.map((a, i) => (
                <ApplicationTableRow key={i} application={a} />
              ))}
            </Table.Body>
          </Table>
        </Panel>
      )}
    </>
  )
}

export default ApplicationPanel