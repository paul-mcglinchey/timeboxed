import { Suspense, lazy, useContext } from "react"
import { Navigate, Route, Routes } from "react-router"
import {
  AppLoader,
  SpinnerLoader
} from "."
import { Application, Permission } from "../enums"

const RestrictedRoute = lazy(() => import("./RestrictedRoute"))
const AdminPanel = lazy(() => import("./AdminPanel/AdminPanel"))
const ClientDashboard = lazy(() => import("./ClientManager/ClientDashboard"))
const ClientManager = lazy(() => import("./ClientManager/ClientManager"))
const ClientPage = lazy(() => import("./ClientManager/ClientPage"))

const GroupDashboard = lazy(() => import("./Groups/GroupDashboard"))
const Dashboard = lazy(() => import("./Dashboard/Dashboard"))
const RotaDashboard = lazy(() => import("./RotaManager/RotaDashboard"))
const EmployeeDashboard = lazy(() => import("./RotaManager/EmployeeDashboard"))
const RotaManager = lazy(() => import("./RotaManager/RotaManager"))
const RotaPage = lazy(() => import("./RotaManager/RotaPage"))

import { AuthContext } from "../contexts/AuthContext"

const PrivateApp = () => {

  const { isAdmin } = useContext(AuthContext)

  return (
    <AppLoader>
      <Suspense fallback={<SpinnerLoader />}>
        <Routes>
          <Route path="/" element={<Navigate to="/dashboard" />} />
          <Route path="/dashboard" element={
            <Dashboard />
          }/>
          <Route path="/groups/*" element={
            <GroupDashboard />
          }/>

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
      </Suspense>
    </AppLoader>
  )
}

export default PrivateApp