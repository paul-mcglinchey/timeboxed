import { IGroupUserService } from "./interfaces"
import { useAsyncHandler, useRequestBuilderService, useResolutionService } from "."
import { endpoints } from "../config"
import { Dispatch, SetStateAction, useCallback } from "react"
import { IFilter, IGroupUser, IGroupUserInviteRequest, IGroupUserRequest } from "../models"
import { IApiError } from "../models/error.model"
import { IListResponse } from "../models/list-response.model"

const useGroupUserService = (setIsLoading: Dispatch<SetStateAction<boolean>>, setError: Dispatch<SetStateAction<IApiError | undefined>>): IGroupUserService => {

  const { asyncHandler, asyncReturnHandler } = useAsyncHandler(setIsLoading)
  const { buildRequest, buildQuery } = useRequestBuilderService()
  const { handleResolution } = useResolutionService()

  const getGroupUsers = useCallback(asyncReturnHandler<IListResponse<IGroupUser>>(async (groupId: string, filters: IFilter[] = []) => {
    const res = await fetch(
      `${endpoints.groupusers(groupId)}${buildQuery(filters)}`,
      buildRequest('GET'))
    const json: IListResponse<IGroupUser> = await res.json()

    return json
  }), [buildRequest])

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

  return { getGroupUsers, updateGroupUser, inviteGroupUser, uninviteGroupUser, joinGroup }
}

export default useGroupUserService