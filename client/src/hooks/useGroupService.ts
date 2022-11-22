import { useContext } from "react"
import { IGroup, IGroupRequest } from "../models"
import { GroupContext } from "../contexts"
import { endpoints } from '../config'
import { useRequestBuilder, useAsyncHandler, useResolutionService } from '.'
import { IGroupService } from "./interfaces"

const useGroupService = (): IGroupService => {
  const groupContext = useContext(GroupContext)
  const { groups, setGroups, setCount, setIsLoading } = groupContext
  
  const { requestBuilder } = useRequestBuilder()
  const { asyncHandler } = useAsyncHandler(setIsLoading)
  const { handleResolution } = useResolutionService()

  const getGroup = (groupId: string | undefined): IGroup | undefined => {
    return groups.find((group: IGroup) => group.id === groupId)
  }

  const addGroup = asyncHandler(async (values: IGroupRequest) => {
    const res = await fetch(endpoints.groups, requestBuilder('POST', undefined, values))
    const json = await res.json()

    handleResolution(res, json, 'create', 'group', [() => addGroupInContext(json)])
  })

  const updateGroup = asyncHandler(async (values: IGroupRequest, groupId: string | undefined) => {
    if (!groupId) throw new Error('Group ID not set')

    const res = await fetch(endpoints.group(groupId), requestBuilder('PUT', undefined, values))
    const json = await res.json()

    console.log(groupId === json.id)

    handleResolution(res, json, 'update', 'group', [() => updateGroupInContext(groupId, json)])
  })

  const deleteGroup = asyncHandler(async (groupId: string | undefined) => {
    if (!groupId) throw new Error('Group ID not set')
    
    const res = await fetch(endpoints.group(groupId), requestBuilder("DELETE"))
    const json = await res.json()

    handleResolution(res, json, 'delete', 'group', [() => deleteGroupInContext(groupId)])
  })

  const addGroupInContext = (group: IGroup) => {
    setGroups(groups => [...groups, group])
    setCount(count => count + 1)
  }

  const deleteGroupInContext = (groupId: string) => {
    setGroups(groups => groups.filter(g => g.id !== groupId))
    setCount(count => count - 1)
  }

  const updateGroupInContext = (groupId: string, values: IGroup) => {
    console.log(values)
    setGroups(groups => groups.map(g => g.id === groupId ? { ...g, ...values } : g))
  }

  return { ...groupContext, getGroup, addGroup, updateGroup, deleteGroup }
}

export default useGroupService