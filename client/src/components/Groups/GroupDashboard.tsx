import { useState } from "react"
import { dashboardLinks } from "../../config"
import { Toolbar, NavMenu, Button } from "../Common"
import { GroupModal, GroupInvitesModal, GroupList } from "."

const Groups = () => {

  const [addGroupOpen, setAddGroupOpen] = useState(false)
  const [groupInvitesOpen, setGroupInvitesOpen] = useState(false)

  return (
    <>
      <NavMenu links={dashboardLinks} hideGroupSelector />
      <div className="px-2 sm:px-6 lg:px-8">
        <Toolbar title="Group management">
          <>
            <Button buttonType="Tertiary" content="Invites" action={() => setGroupInvitesOpen(true)} />
            <Button buttonType="Toolbar" content="Create group" action={() => setAddGroupOpen(true)} />
          </>
        </Toolbar>
        <GroupList setAddGroupOpen={setAddGroupOpen} />
      </div>
      <GroupModal isOpen={addGroupOpen} close={() => setAddGroupOpen(false)} />
      <GroupInvitesModal isOpen={groupInvitesOpen} close={() => setGroupInvitesOpen(false)} />
    </>
  )
}

export default Groups