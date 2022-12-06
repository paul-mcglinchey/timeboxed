import { useEffect, useState } from "react"
import { endpoints } from "../../config"
import { useGroupUserService, useRequestBuilderService } from "../../hooks"
import { IGroupList } from "../../models"
import { IApiError } from "../../models/error.model"
import { IListResponse } from "../../models/list-response.model"
import { InlineButton, SpinnerLoader } from "../Common"

const GroupInvites = () => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()
  const [invites, setInvites] = useState<IGroupList[]>([])

  const { buildRequest } = useRequestBuilderService()
  const { joinGroup } = useGroupUserService(setIsLoading, setError)

  useEffect(() => {
    const _fetch = async () => {
      const res = await fetch(endpoints.groupinvites, buildRequest())
      const json: IListResponse<IGroupList> = await res.json()

      setInvites(json.items)
    }

    _fetch()
  }, [buildRequest])

  return (
    <>
      {invites && invites.length > 0 ? (
        <div>
          {invites.map((g, i) => (
            <div key={i} className="flex justify-between">
              <div>{g.name}</div>
              <div>
                <InlineButton action={() => joinGroup(g.id)}>Join</InlineButton>
              </div>
            </div>
          ))}
        </div>
      ) : (
        isLoading ? <SpinnerLoader /> : error ? Error : null
      )}
    </>
  )
}

export default GroupInvites