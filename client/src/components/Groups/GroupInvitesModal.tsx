import { useContext } from "react"
import { GroupContext } from "../../contexts"
import { useGroupUserService } from "../../hooks"
import { InlineButton, Modal, SpinnerLoader } from "../Common"

interface IGroupInvitesModalProps {
  isOpen: boolean
  close: () => void
}

const GroupInvitesModal = ({ isOpen, close }: IGroupInvitesModalProps) => {

  const { invites, isLoading } = useContext(GroupContext)
  const { joinGroup } = useGroupUserService()

  return (
    <Modal
      title="Group invites"
      description="This dialog allows for pending group invites to be viewed and managed."
      isOpen={isOpen}
      close={close}
    >
      {() => (
        isLoading ? (
          <SpinnerLoader />
        ) : (
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
        )
      )}
    </Modal>
  )
}

export default GroupInvitesModal