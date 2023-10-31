import { useCallback, useEffect, useState } from "react"
import { SearchBar, SpinnerLoader, Table } from ".."
import { IFilter, IFilterableField, IGroup, ITableHeaderItem } from "../../models"
import { endpoints } from "../../config"
import { useRequestBuilderService } from "../../hooks"
import { IListResponse } from "../../models/list-response.model"
import { InformationCircleIcon } from "@heroicons/react/20/solid"
import GroupTableRow from "./GroupTableRow"
import Panel from "./Panel"

const filters: IFilterableField[] = [
  { label: 'Group name', name: 'name' }
]

const headers: ITableHeaderItem[] = [
  { name: 'Name' },
  { name: 'Description' },
  { name: 'Applications' },
  { name: 'Users' },
  { name: 'Options', hugRight: true },
]

const GroupPanel = () => {
  
  const { buildRequest } = useRequestBuilderService()

  const [filter, setFilter] = useState<IFilter>({ value: null, label: null, name: null })
  const [groups, setGroups] = useState<IGroup[]>([])
  const [count, setCount] = useState<number>(0)
  const [loading, setLoading] = useState<boolean>(false)

  const fetchGroups = useCallback(async () => {
    setLoading(true)

    const res = await fetch(`${endpoints.admin.groups}?${filter.name}=${filter.value}`, buildRequest('GET'))
    const json: IListResponse<IGroup> = await res.json()
    
    setGroups(json.items)
    setCount(json.count)

    setLoading(false)
  }, [filter])

  useEffect(() => {
    // if the filter has a value then fetch groups
    if (filter.value && filter.value.trim().length > 0) {
      fetchGroups()
    } else {
      setGroups([])
      setCount(0)
    }
  }, [filter, fetchGroups])

  return (
    <Panel
      title="Groups"
      hideSave
    >
      <SearchBar setFilter={setFilter} initialFilterField={filters[0]} filterableFields={filters} />
      {(count > 0) ? (
        <Table isLoading={loading}>
          <Table.Header headers={headers} />
          <Table.Body>
            {groups.map(g => (
              <GroupTableRow key={g.id} group={g} fetchGroups={fetchGroups}/>
            ))}
          </Table.Body>
        </Table>
      ) : (
        loading ? (
          <div className="my-4">
            <SpinnerLoader className="w-6 h-6"/>
          </div>
        ) : (
          <div className="flex items-center justify-between px-4 py-2 my-4 bg-blue-200/40 font-bold rounded-md flex-row-reverse"><InformationCircleIcon className="w-5 h-5" /> Use the search bar to find groups</div>
        )
      )}
    </Panel>
  )
}

export default GroupPanel