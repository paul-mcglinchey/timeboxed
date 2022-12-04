import { Outlet } from "react-router"
import { rotaLinks } from "../../config";
import { EmployeeProvider, GroupContext, RotaProvider } from "../../contexts";
import { NavMenu, SpinnerLoader } from "../Common";
import { useContext } from "react";

const RotaManager = () => {

  const { currentGroup, isLoading } = useContext(GroupContext)

  return (
    <>
      <NavMenu links={rotaLinks} />
      <div className="px-2 sm:px-6 lg:px-8">
        {isLoading || !currentGroup ? (
          <SpinnerLoader />
        ) : (
          <RotaProvider>
            <EmployeeProvider>
              <Outlet />
            </EmployeeProvider>
          </RotaProvider>
        )}
      </div>
    </>
  )
}

export default RotaManager;