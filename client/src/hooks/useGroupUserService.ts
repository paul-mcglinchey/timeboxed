import { IGroupUserService } from "./interfaces"
import { useAsyncHandler, useRequestBuilderService, useResolutionService } from "."
import { endpoints } from "../config"
import { Dispatch, SetStateAction } from "react"
import { IGroupUserInviteRequest, IGroupUserRequest } from "../models"
import { IApiError } from "../models/error.model"

const useGroupUserService = (setIsLoading: Dispatch<SetStateAction<boolean>>, setError: Dispatch<SetStateAction<IApiError | undefined>>): IGroupUserService => {

  const { asyncHandler } = useAsyncHandler(setIsLoading)
  const { buildRequest } = useRequestBuilderService()
  const { handleResolution } = useResolutionService()

  const updateGroupUser = asyncHandler(async (groupId: string | undefined, userId: string | undefined, values: IGroupUserRequest) => {
    if (!groupId || !userId) throw new Error('Something went wrong...')

    const res = await fetch(endpoints.groupuser(groupId, userId), buildRequest('PUT', undefined, values))
    const json = await res.json()

    handleResolution(res, json, 'update', 'group user')
  })

  const inviteGroupUser = asyncHandler(async (groupId: string | undefined, values: IGroupUserInviteRequest) => {
    if (!groupId) throw new Error('Something went wrong...')

    const res = await fetch(endpoints.invitegroupuser(groupId), buildRequest('POST', undefined, values))
    if (!res.ok && res.status < 500) return setError(await res.json())

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

  return { updateGroupUser, inviteGroupUser, uninviteGroupUser, joinGroup }
}

export default useGroupUserService