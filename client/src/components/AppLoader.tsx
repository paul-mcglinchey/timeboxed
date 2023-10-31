import { Transition } from "@headlessui/react"
import { Dispatch, ReactNode, SetStateAction, useContext, useEffect, useState } from "react"
import { AuthContext } from "../contexts/AuthContext"
import { GroupContext, GroupProvider } from "../contexts/GroupContext"
import { MetaInfoContext, MetaInfoProvider } from "../contexts/MetaInfoContext"
import { IChildrenProps } from "../models"
import { combineClassNames } from "../services"
import { WideIcon } from "./Common"

const AppLoader = ({ children }: { children: ReactNode }) => {

  const [authCompleted, setAuthCompleted] = useState<boolean>(false)
  const [loadingCompleted, setLoadingCompleted] = useState<boolean>(false)
  const [appLoading, setAppLoading] = useState<boolean>(true)

  const { isLoading: isAuthLoading, user } = useContext(AuthContext)

  useEffect(() => {
    if (!isAuthLoading) setTimeout(() => setAuthCompleted(true), 100)
  }, [isAuthLoading])

  useEffect(() => {
    loadingCompleted && setTimeout(() => setAppLoading(false), 500)
  }, [loadingCompleted, setAppLoading])

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
          <MetaInfoProvider>
            <SubLoader setLoadingCompleted={setLoadingCompleted}>
              {!appLoading && children}
            </SubLoader>
          </MetaInfoProvider>
        </GroupProvider>
      )}
    </>
  )
}

interface ISubLoaderProps {
  setLoadingCompleted: Dispatch<SetStateAction<boolean>>
}

const SubLoader = ({ children, setLoadingCompleted }: ISubLoaderProps & IChildrenProps) => {

  const { isLoading: isGroupsLoading } = useContext(GroupContext)
  const { isLoading: isMetaInfoLoading } = useContext(MetaInfoContext)

  useEffect(() => {
    if (!isGroupsLoading && !isMetaInfoLoading) setLoadingCompleted(true)
  }, [isGroupsLoading, isMetaInfoLoading, setLoadingCompleted])

  return (
    <>
      {children}
    </>
  )
}

export default AppLoader