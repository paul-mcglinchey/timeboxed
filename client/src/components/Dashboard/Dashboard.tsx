import { useContext, useEffect, useState } from "react";
import { dashboardLinks } from "../../config";
import { NavMenu, SpinnerLoader, Toolbar } from "../Common";
import { useApplicationService, useGroupService } from "../../hooks";
import { IApplication } from "../../models";

import GroupPrompter from "../Groups/GroupPrompter";
import GroupModal from "../Groups/GroupModal";
import AppCard from "./AppCard";
import { AuthContext } from "../../contexts";
import { IApiError } from "../../models/error.model";

const Dashboard = () => {
  const [loading, setLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError | undefined>()
  const [applications, setApplications] = useState<IApplication[]>([])

  const [addGroupOpen, setAddGroupOpen] = useState<boolean>(false);

  const { user } = useContext(AuthContext)
  const { fetchGroups, count, currentGroup } = useGroupService(setLoading, setError)
  const { getApplication } = useApplicationService()

  const setUserAccessibleApps = (): void => {
    if (!currentGroup) return setApplications([])

    let groupApps = currentGroup.applications
    let userApps = currentGroup.users.find(gu => gu.userId === user?.id)?.applications || []

    let userAccessibleAppIds = userApps.filter(ua => groupApps.includes(ua))
    let userAccessibleApps = userAccessibleAppIds.map(uaa => getApplication(uaa)).filter((a): a is IApplication => !!a)

    setApplications(userAccessibleApps)
  }

  useEffect(() => {
    fetchGroups()
  }, [])

  useEffect(() => {
    setUserAccessibleApps()
  }, [currentGroup])
  
  return (
    <>
      <NavMenu links={dashboardLinks} />
      <div className="px-2 sm:px-6 lg:px-8">
        {count > 0 ? (
          <>
            <Toolbar title="Applications" />
            <div className="grid grid-cols-1 xl:grid-cols-2 3xl:grid-cols-3 tracking-wide gap-4 md:gap-8">
              {applications.map((a, i) => (
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
          loading ? (
            <SpinnerLoader />
          ) : (
            error ? (
              <span>{error.message}</span>
            ) : (
              <GroupPrompter action={() => setAddGroupOpen(true)} />
            )
          )
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