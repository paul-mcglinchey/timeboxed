import { useContext } from "react"
import { Navigate } from "react-router"
import { MetaInfoContext } from "../contexts/MetaInfoContext"
import { Permission } from "../enums"

interface IRestrictedRouteProps {
  applicationId: number
  permission: Permission
  component: JSX.Element
  redirect?: string
}

const RestrictedRoute = ({ applicationId, permission, component, redirect = "/notpermitted" }: IRestrictedRouteProps): JSX.Element => {
  const { hasPermission } = useContext(MetaInfoContext)

  return hasPermission(applicationId, permission) ? component : <Navigate to={redirect} />
}

export default RestrictedRoute