import { Outlet } from "react-router"
import { useGroupService } from "../../hooks";
import { rotaLinks } from "../../config";
import { EmployeeProvider, RotaProvider } from "../../contexts";
import { NavMenu, SpinnerLoader } from "../Common";

const RotaManager = () => {

  const { isLoading, currentGroup } = useGroupService()

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