import { Navigate } from "react-router"
import { Permission } from "../enums"
import { useAuthService } from "../hooks"

interface IRestrictedRouteProps {
  applicationId: number
  permission: Permission
  component: JSX.Element
  redirect?: string
}

const RestrictedRoute = ({ applicationId , permission, component, redirect = "/notpermitted" }: IRestrictedRouteProps): JSX.Element => {
  const { hasPermission } = useAuthService()

  return hasPermission(applicationId, permission) ? component : <Navigate to={redirect} />
}

export default RestrictedRoute