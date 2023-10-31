import { Outlet } from "react-router"
import { clientLinks } from "../../config"
import { ClientProvider } from "../../contexts/ClientContext"
import { NavMenu } from "../Common";

const ClientManager = () => {
  return (
    <>
      <NavMenu links={clientLinks} />
      <div className="px-2 sm:px-6 lg:px-8">
        <ClientProvider>
          <Outlet />
        </ClientProvider>
      </div>
    </>
  )
}

export default ClientManager;