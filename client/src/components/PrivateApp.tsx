import { Navigate, Route, Routes } from "react-router"
import { useAuthService } from "../hooks"
import {
  AdminPanel,
  ClientDashboard,
  ClientManager,
  ClientPage,
  Dashboard,
  GroupDashboard,
  EmployeeDashboard,
  RotaDashboard,
  RotaManager,
  RotaPage,
  AppLoader,
  RestrictedRoute
} from "."
import { Application, Permission } from "../enums"

const PrivateApp = () => {

  const { isAdmin } = useAuthService()

  return (
    <AppLoader>
      <Routes>
        <Route path="/" element={<Navigate to="/dashboard" />} />
        <Route path="/dashboard" element={
          <Dashboard />
        } />
        <Route path="/groups/*" element={
          <GroupDashboard />
        } />

        {/* Client manager specific routes */}
        <Route path="/clients/*" element={
          <RestrictedRoute applicationId={Application.ClientManager} permission={Permission.ApplicationAccess} component={<ClientManager />} />
        }>
          <Route path="dashboard" element={<ClientDashboard />} />
          <Route path=":clientId/*" element={<ClientPage />} />
        </Route>

        {/* Rota manager specific routes */}
        <Route path="/rotas/*" element={
          <RestrictedRoute applicationId={Application.RotaManager} permission={Permission.ApplicationAccess} component={<RotaManager />} />
        }>
          <Route path="dashboard" element={<RotaDashboard />} />
          <Route path=":rotaId/*" element={<RotaPage />} />
          <Route path="employees" element={<EmployeeDashboard />} />
          <Route path="employees/:isAddEmployeeOpen" element={<EmployeeDashboard />} />
        </Route>

        {/* Admin Routes */}
        <Route path="adminpanel/*" element={
          isAdmin() ? <AdminPanel /> : <Navigate to="/" />
        } />
      </Routes>
    </AppLoader>
  )
}

export default PrivateApp