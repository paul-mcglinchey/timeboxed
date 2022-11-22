import { IListCollection } from "../models"
import { endpoints } from "../config"
import { useAsyncHandler, useNotification, useRequestBuilder, useResolutionService } from '.'
import { Notification } from "../enums"
import { useState } from "react"

const useListCollectionService = (refresh: () => void = () => {}) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)

  const { addNotification } = useNotification()
  const { requestBuilder } = useRequestBuilder()
  const { asyncHandler } = useAsyncHandler(setIsLoading)
  const { handleResolution } = useResolutionService()

  const init = asyncHandler(async () => {
    const res = await fetch(endpoints.systemlistcollections, requestBuilder('POST', undefined, { lists: [] }))
    const json = await res.json()

    handleResolution(res, json, 'initialize', 'system list collection')
  })

  const update = asyncHandler(async (listcollectionId: string | undefined, values: IListCollection) => {
    if (!listcollectionId) return addNotification('Something went wrong...', Notification.Error)

    const res = await fetch(endpoints.systemlistcollection(listcollectionId), requestBuilder('PUT', undefined, values))
    const json = await res.json()

    if (res.ok) {
      addNotification(`${res.status}: Successfully updated system list collection`, Notification.Success)
      return refresh()
    }

    const message = `${res.status}: ${res.status < 500 ? (json.message || res.statusText) : 'A problem occurred updating system list collection'}`
    return addNotification(message, Notification.Error)
  })

  return { init, update, isLoading }
}

export default useListCollectionService