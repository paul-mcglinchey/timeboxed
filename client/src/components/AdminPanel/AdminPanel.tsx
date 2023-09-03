import { NavMenu, SmartLink, Toolbar } from "../Common";
import { SystemListCollectionPanel, ApplicationPanel, PermissionPanel, GroupPanel } from ".";
import { Navigate, PathMatch, Route, Routes } from "react-router";
import { IChildrenProps } from "../../models";
import { combineClassNames } from "../../services";

const AdminPanel = () => {
  return (
    <>
      <NavMenu hideGroupSelector />
      <div className="px-2 sm:px-6 lg:px-8 pb-10">
        <Toolbar title='Admin panel' />
        <div className="inline-flex space-x-2 mb-4">
          <TabLink to="lists">Lists</TabLink>
          <TabLink to="applications">Applications</TabLink>
          <TabLink to="permissions">Permissions</TabLink>
          <TabLink to="groups">Groups</TabLink>
        </div>
        <Routes>
          <Route path="lists" element={<SystemListCollectionPanel />} />
          <Route path="applications" element={<ApplicationPanel />} />
          <Route path="permissions" element={<PermissionPanel />} />
          <Route path="groups" element={<GroupPanel />} />
          <Route path="/" element={<Navigate to="lists" />} />
        </Routes>
      </div>
    </>
  )
}

const TabLink = ({ to, children }: { to: string } & IChildrenProps) => {
  return (
    <SmartLink
      to={to}
      className={(match: PathMatch<string> | null) => combineClassNames(
        match ? "border-blue-500" : "border-transparent", "py-1 px-2 text-xl transition-all border-b-2"
      )}
    >
      {children}
    </SmartLink>
  )
}

export default AdminPanel