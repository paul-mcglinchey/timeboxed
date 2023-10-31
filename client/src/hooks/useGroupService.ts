import { Dispatch, SetStateAction, useContext } from "react"
import { IGroup, IGroupRequest } from "../models"
import { GroupContext } from "../contexts/GroupContext"
import { endpoints } from '../config'
import { useRequestBuilderService, useAsyncHandler, useResolutionService } from '.'
import { IGroupService } from "./interfaces"
import { IApiError } from "../models/error.model"
import { IAdminGroupRequest } from "../models/admin-group.model"

const useGroupService = (setIsLoading: Dispatch<SetStateAction<boolean>>, setError: Dispatch<SetStateAction<IApiError | undefined>>): IGroupService => {

  const groupContext = useContext(GroupContext)
  const { setGroups, setCount } = groupContext
  
  const { buildRequest } = useRequestBuilderService()
  const { asyncHandler, asyncReturnHandler } = useAsyncHandler(setIsLoading)
  const { handleResolution } = useResolutionService()

  const getGroupFromContext = (groupId: string): IGroup | undefined => groupContext.groups.find(g => g.id === groupId);

  const getGroup = asyncReturnHandler<IGroup>(async (groupId: string) => {
    const res = await fetch(endpoints.admin.group(groupId), buildRequest('GET'))
    const json: IGroup = await res.json()

    return json
  })

  const addGroup = asyncHandler(async (values: IGroupRequest) => {
    const res = await fetch(endpoints.groups, buildRequest('POST', undefined, values))
    const json = await res.json()

    handleResolution(res, json, 'create', 'group', [() => addGroupInContext(json)])
  })

  const updateGroup = asyncHandler(async (values: IGroupRequest, groupId: string | undefined) => {
    if (!groupId) throw new Error('Group ID not set')

    const res = await fetch(endpoints.group(groupId), buildRequest('PUT', undefined, values))
    if (!res.ok && res.status < 500) return setError(await res.json())
    const json = await res.json()

    handleResolution(res, json, 'update', 'group', [() => updateGroupInContext(groupId, json)])
  })

  const adminUpdateGroup = asyncHandler(async (values: IAdminGroupRequest, groupId: string) => {
    const res = await fetch(endpoints.admin.group(groupId), buildRequest('PUT', undefined, values))
    if (!res.ok && res.status < 500) return setError(await res.json())

    handleResolution(res, null, 'update', 'group')
  })

  const deleteGroup = asyncHandler(async (groupId: string | undefined) => {
    if (!groupId) throw new Error('Group ID not set')
    
    const res = await fetch(endpoints.group(groupId), buildRequest("DELETE"))
    const json = await res.json()

    handleResolution(res, json, 'delete', 'group', [() => deleteGroupInContext(groupId)])
  })

  const addGroupInContext = (group: IGroup) => {
    setGroups(groups => [
      ...groups, group
    ])
    setCount(count => count + 1)
  }

  const deleteGroupInContext = (groupId: string) => {
    setGroups(groups => groups.filter(g => g.id !== groupId))
    setCount(count => count - 1)
  }

  const updateGroupInContext = (groupId: string, values: IGroup) => {
    setGroups(groups => groups.map(g => g.id === groupId ? values : g))
  }

  return { ...groupContext, getGroupFromContext, getGroup, addGroup, updateGroup, adminUpdateGroup, deleteGroup }
}

export default useGroupService