import { Transition } from "@headlessui/react"
import { Dispatch, SetStateAction, useContext, useEffect, useState } from "react"
import { GroupProvider, MetaInfoContext, MetaInfoProvider, UserProvider } from "../contexts"
import { useAuthService, useGroupService, useUserService } from "../hooks"
import { IChildrenProps } from "../models"
import { combineClassNames } from "../services"
import { WideIcon } from "./Common"

const AppLoader = ({ children }: { children: any }) => {

  const [authCompleted, setAuthCompleted] = useState<boolean>(false)
  const [loadingCompleted, setLoadingCompleted] = useState<boolean>(false)
  const [appLoading, setAppLoading] = useState<boolean>(true)

  const { isLoading: isAuthLoading, user } = useAuthService()

  useEffect(() => {
    if (!isAuthLoading) setAuthCompleted(true)
  }, [isAuthLoading])

  return (
    <>
      <Transition
        show={appLoading}
        enter=""
        enterFrom="opacity-0"
        enterTo="opacity-100"
        leave="ease-in-out duration-200"
        leaveFrom="opacity-100"
        leaveTo="opacity-0"
      >
        <div className="fixed justify-center items-center flex z-50 w-screen h-screen bg-gray-900 overflow-hidden inset-0">
          <div className="flex flex-col space-y-4 transform -translate-y-12">
            <WideIcon className="h-16 md:h-24 w-auto" />
            <div className="w-full bg-gray-800 rounded h-3">
              <div className={combineClassNames(
                "h-full rounded-lg bg-blue-500 transition-all duration-500",
                !authCompleted && !loadingCompleted && "w-0", authCompleted && !loadingCompleted && "w-1/5", authCompleted && loadingCompleted && "w-full"
              )} />
            </div>
          </div>
        </div>
      </Transition>
      {authCompleted && user && (
        <GroupProvider>
          <UserProvider>
            <MetaInfoProvider>
              <SubLoader setAppLoading={setAppLoading} loadingCompleted={loadingCompleted} setLoadingCompleted={setLoadingCompleted}>
                {!appLoading && children}
              </SubLoader>
            </MetaInfoProvider>
          </UserProvider>
        </GroupProvider>
      )}
    </>
  )
}

interface ISubLoaderProps {
  setAppLoading: Dispatch<SetStateAction<boolean>>
  loadingCompleted: boolean
  setLoadingCompleted: Dispatch<SetStateAction<boolean>>
}

const SubLoader = ({ children, setAppLoading, loadingCompleted, setLoadingCompleted }: ISubLoaderProps & IChildrenProps) => {
  
  const { isLoading: isGroupsLoading } = useGroupService()
  const { isLoading: isUsersLoading } = useUserService()
  const { isLoading: isMetaInfoLoading } = useContext(MetaInfoContext)
  
  useEffect(() => {
    if (!isGroupsLoading && !isUsersLoading && !isMetaInfoLoading) setLoadingCompleted(true)
  }, [isGroupsLoading, isUsersLoading, isMetaInfoLoading, setLoadingCompleted])

  useEffect(() => {
    loadingCompleted && setTimeout(() => setAppLoading(false), 500)
  }, [loadingCompleted, setAppLoading])

  return (
    <>
      {children}
    </>
  )
}

export default AppLoader