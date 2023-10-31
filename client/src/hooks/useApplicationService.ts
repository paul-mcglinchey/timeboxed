import { IApplication } from "../models"
import { IApplicationService } from "./interfaces"
import { useContext } from "react"
import { MetaInfoContext } from "../contexts/MetaInfoContext"

const useApplicationService = (): IApplicationService => {
  const metaInfoContext = useContext(MetaInfoContext)
  const { applications = [] } = metaInfoContext

  const getApplication = (applicationId: number | undefined): IApplication | undefined => {
    return applications.find(a => a.id === applicationId)
  }

  return { applications, getApplication }
}

export default useApplicationService