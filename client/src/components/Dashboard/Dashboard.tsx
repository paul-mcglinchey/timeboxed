import { useState } from "react";
import { dashboardLinks } from "../../config";
import { NavMenu, Toolbar } from "../Common";
import { AppCard } from ".";
import { useApplicationService, useAuthService, useGroupService } from "../../hooks";
import { IApplication } from "../../models";
import { GroupModal, GroupPrompter } from "../Groups";

const Dashboard = () => {

  const [addGroupOpen, setAddGroupOpen] = useState<boolean>(false);

  const { user } = useAuthService()
  const { count, currentGroup } = useGroupService()
  const { getApplication } = useApplicationService()

  const getUserAccessibleApps = (): IApplication[] => {
    if (!currentGroup) return []

    let groupApps = currentGroup.applications
    let userApps = currentGroup.groupUsers.find(gu => gu.userId === user?.id)?.applications || []

    let userAccessibleApps = userApps.filter(ua => groupApps.includes(ua));

    return userAccessibleApps.map(uaa => getApplication(uaa)).filter((a): a is IApplication => !!a)
  }

  return (
    <>
      <NavMenu links={dashboardLinks} />
      <div className="px-2 sm:px-6 lg:px-8">
        {count > 0 ? (
          <>
            <Toolbar title="Applications" />
            <div className="grid grid-cols-1 xl:grid-cols-2 3xl:grid-cols-3 tracking-wide gap-4 md:gap-8">
              {getUserAccessibleApps().map((a, i) => (
                <AppCard
                  title={a.name || '--'}
                  backgroundImage={a.backgroundImage}
                  backgroundVideo={a.backgroundVideo}
                  href={a.url || '/dashboard'}
                  subtitle={a.description}
                  key={i}
                />
              ))}
            </div>
          </>
        ) : (
          <>
            <GroupPrompter action={() => setAddGroupOpen(true)} />
          </>
        )}
      </div>
      <GroupModal 
        isOpen={addGroupOpen}
        close={() => setAddGroupOpen(false)}
      />
    </>
  )
}

export default Dashboard;