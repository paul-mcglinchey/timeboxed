import { useContext, useEffect, useState } from "react"
import { endpoints } from "../../config"
import { GroupContext } from "../../contexts"
import { useGroupUserService, useRequestBuilderService } from "../../hooks"
import { IGroupList } from "../../models"
import { IListResponse } from "../../models/list-response.model"
import { InlineButton, Modal, SpinnerLoader } from "../Common"

interface IGroupInvitesModalProps {
  isOpen: boolean
  close: () => void
}

const GroupInvitesModal = ({ isOpen, close }: IGroupInvitesModalProps) => {

  const [invites, setInvites] = useState<IGroupList[]>([])

  const { isLoading } = useContext(GroupContext)
  const { joinGroup } = useGroupUserService()
  const { buildRequest } = useRequestBuilderService()

  useEffect(() => {
    const _fetch = async () => {
      const res = await fetch(endpoints.groupinvites, buildRequest())
      const json: IListResponse<IGroupList> = await res.json()

      setInvites(json.items)
    }

    _fetch()
  }, [buildRequest])

  return (
    <Modal
      title="Group invites"
      description="This dialog allows for pending group invites to be viewed and managed."
      isOpen={isOpen}
      close={close}
    >
      {() => (
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
            isLoading && <SpinnerLoader />
          )}
        </>
      )}
    </Modal>
  )
}

export default GroupInvitesModal