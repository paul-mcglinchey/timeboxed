import { IGroupUserRequest, IRole } from "../../models"
import { Switch } from "../Common"

interface IUserRoleGroupProps {
  values: IGroupUserRequest
  onChange: (groupUser: IGroupUserRequest) => void
  label?: string | undefined
  roles: IRole[]
  applicationId?: number | undefined
}

const UserRoleGroup = ({ values, label, roles, applicationId, onChange }: IUserRoleGroupProps) => {
  const applicationEnabled = (): boolean => applicationId !== undefined && values.applications.includes(applicationId)
  const roleEnabled = (roleId: string): boolean => values.roles.includes(roleId)

  const toggleApplication = (): void => {
    const hasApplication = applicationEnabled()
    applicationId && onChange({ ...values, applications: hasApplication ? values.applications.filter(a => a !== applicationId) : [...values.applications, applicationId] })
  }
  
  const toggleRole = (roleId: string): void => {
    const hasRole = roleEnabled(roleId)
    onChange({ ...values, roles: hasRole ? values.roles.filter(r => r !== roleId) : [...values.roles, roleId] })
  }

  return (
    <div>
      <div className="flex justify-between items-center">
      <h3 className="text-xl font-bold mb-2">
        {label || "Role Group"}
      </h3>
      {applicationId && (
        <Switch enabled={applicationEnabled()} setEnabled={() => toggleApplication()} description="User application access control"/>
      )}
      </div>
      <div className="grid gap-2">
        {roles.map((r, k) => (
          <div key={k} className="flex p-2 justify-between bg-gray-900 rounded-lg">
            <div>
              {r?.name}
            </div>
            <Switch enabled={roleEnabled(r.id)} setEnabled={() => toggleRole(r.id)} description="User role access control" />
          </div>
        ))}
      </div>
    </div>
  )
}

export default UserRoleGroup