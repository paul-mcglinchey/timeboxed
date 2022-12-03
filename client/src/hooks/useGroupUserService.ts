import { IGroupUserService } from "./interfaces"
import { useAsyncHandler, useGroupService, useRequestBuilderService, useResolutionService } from "."
import { endpoints } from "../config"
import { useState } from "react"
import { IGroupUser, IGroupUserInviteRequest, IGroupUserRequest } from "../models"

const useGroupUserService = (): IGroupUserService => {
  
  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<any>()

  const { setGroups } = useGroupService()
  const { asyncHandler } = useAsyncHandler(setIsLoading, setError)
  const { buildRequest } = useRequestBuilderService()
  const { handleResolution } = useResolutionService()

  const updateGroupUser = asyncHandler(async (groupId: string | undefined, userId: string | undefined, values: IGroupUserRequest) => {
    if (!groupId || !userId) throw new Error('Something went wrong...')

    const res = await fetch(endpoints.groupuser(groupId, userId), buildRequest('PUT', undefined, values))
    const json = await res.json()

    handleResolution(res, json, 'update', 'group user', [() => updateGroupUserInContext(groupId, userId, json)])
  })

  const inviteGroupUser = asyncHandler(async (groupId: string | undefined, values: IGroupUserInviteRequest) => {
    if (!groupId) throw new Error('Something went wrong...')

    const res = await fetch(endpoints.invitegroupuser(groupId), buildRequest('POST', undefined, values))
    if (!res.ok && res.status < 500) throw new Error(await res.text())

    const json = await res.json()

    handleResolution(res, json, 'invite', 'group user')
  })

  const uninviteGroupUser = asyncHandler(async (groupId: string | undefined, userId: string | undefined) => {
    if (!groupId || !userId) throw new Error('Something went wrong...')

    const res = await fetch(endpoints.uninvitegroupuser(groupId, userId), buildRequest('POST'))
    if (!res.ok && res.status < 500) throw new Error(await res.text())

    const json = await res.json()

    handleResolution(res, json, 'uninvite', 'group user')
  })

  const joinGroup = asyncHandler(async (groupId: string | undefined) => {
    if (!groupId) throw new Error('Group ID not set')

    const res = await fetch(endpoints.joingroup(groupId), buildRequest('PUT'));
    const json = await res.json()

    handleResolution(res, json, 'join', 'group')
  })

  const updateGroupUserInContext = (groupId: string, userId: string, values: IGroupUser) => {
    setGroups(groups => groups.map(
      g => g.id === groupId
        ? ({ ...g, groupUsers: g.groupUsers.map(
          gu => gu.userId === userId 
            ? ({ ...gu, ...values })
            : gu
          )}) 
        : g
      ))
  }

  return { updateGroupUser, inviteGroupUser, uninviteGroupUser, joinGroup, isLoading, error }
}

export default useGroupUserService