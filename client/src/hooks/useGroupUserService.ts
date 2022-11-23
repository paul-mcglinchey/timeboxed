import { IGroupUserService } from "./interfaces"
import { useAsyncHandler, useGroupService, useRequestBuilderService, useResolutionService } from "."
import { endpoints } from "../config"
import { useState } from "react"
import { IGroupUser, IGroupUserRequest } from "../models"

const useGroupUserService = (): IGroupUserService => {
  
  const [isLoading, setIsLoading] = useState<boolean>(false)

  const { setGroups } = useGroupService()
  const { asyncHandler } = useAsyncHandler(setIsLoading)
  const { buildRequest } = useRequestBuilderService()
  const { handleResolution } = useResolutionService()

  const updateGroupUser = asyncHandler(async (groupId: string | undefined, userId: string | undefined, values: IGroupUserRequest) => {
    if (!groupId || !userId) throw new Error('Something went wrong...')

    const res = await fetch(endpoints.groupuser(groupId, userId), buildRequest('PUT', undefined, values))
    const json = await res.json()

    handleResolution(res, json, 'update', 'group user', [() => updateGroupUserInContext(groupId, userId, json)])
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

  return { updateGroupUser, joinGroup, isLoading }
}

export default useGroupUserService