import { IListCollection } from "../models"
import { endpoints } from "../config"
import { useAsyncHandler, useNotification, useRequestBuilderService, useResolutionService } from '.'
import { Notification } from "../enums"
import { useState } from "react"

const useListCollectionService = (refresh: () => void = () => {}) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<any>();

  const { addNotification } = useNotification()
  const { buildRequest } = useRequestBuilderService()
  const { asyncHandler } = useAsyncHandler(setIsLoading)
  const { handleResolution } = useResolutionService()

  const init = asyncHandler(async () => {
    const res = await fetch(endpoints.systemlistcollections, buildRequest('POST', undefined, { lists: [] }))
    const json = await res.json()

    handleResolution(res, json, 'initialize', 'system list collection')
  })

  const update = asyncHandler(async (listcollectionId: string | undefined, values: IListCollection) => {
    if (!listcollectionId) return addNotification('Something went wrong...', Notification.Error)

    const res = await fetch(endpoints.systemlistcollection(listcollectionId), buildRequest('PUT', undefined, values))
    if (!res.ok && res.status < 500) return setError(await res.json())
    const json = await res.json()

    if (res.ok) {
      addNotification(`${res.status}: Successfully updated system list collection`, Notification.Success)
      return refresh()
    }

    const message = `${res.status}: ${res.status < 500 ? (json.message || res.statusText) : 'A problem occurred updating system list collection'}`
    return addNotification(message, Notification.Error)
  })

  return { init, update, isLoading, error }
}

export default useListCollectionService