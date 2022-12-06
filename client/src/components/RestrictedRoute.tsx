import { useContext } from "react"
import { Navigate } from "react-router"
import { AuthContext } from "../contexts"
import { Permission } from "../enums"

interface IRestrictedRouteProps {
  applicationId: number
  permission: Permission
  component: JSX.Element
  redirect?: string
}

const RestrictedRoute = ({ applicationId , permission, component, redirect = "/notpermitted" }: IRestrictedRouteProps): JSX.Element => {
  const { hasPermission } = useContext(AuthContext)

  return hasPermission(applicationId, permission) ? component : <Navigate to={redirect} />
}

export default RestrictedRoute