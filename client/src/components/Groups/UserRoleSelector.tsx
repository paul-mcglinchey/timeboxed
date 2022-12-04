import { useApplicationService, useGroupUserService, useRoleService } from "../../hooks"
import { IApplication, IContextualFormProps, IGroup, IGroupUser } from "../../models"
import { Form, Formik } from "formik"

import UserRoleGroup from "./UserRoleGroup"
import { GroupContext } from "../../contexts"
import { useContext } from "react"

interface IUserRoleSelectorProps {
  groupUser: IGroupUser
}

const UserRoleSelector = ({ groupUser, ContextualSubmissionButton }: IUserRoleSelectorProps & IContextualFormProps) => {

  const { getGroup } = useContext(GroupContext)
  const { roles } = useRoleService()
  const { getApplication } = useApplicationService()
  const { updateGroupUser } = useGroupUserService()

  const group: IGroup | undefined = getGroup(groupUser.groupId)

  return (
    <Formik
      initialValues={{
        roles: groupUser.roles || [],
        applications: groupUser.applications || []
      }}
      onSubmit={(values) => {
        updateGroupUser(groupUser.groupId, groupUser.userId, values)
      }}
    >
      {({ values, setFieldValue, isValid }) => (
        <Form>
          <div className="mt-4 grid grid-cols-1 gap-4">
            <UserRoleGroup
              values={values}
              onChange={(groupUser) => {
                setFieldValue('roles', groupUser.roles)
              }}
              roles={roles.filter(r => !r.applicationId)}
              label="Group Roles"
            />
            {group?.applications.map(ga => getApplication(ga)).filter((a): a is IApplication => !!a).map((a, j) => (
              <UserRoleGroup
                key={j}
                values={values}
                onChange={(groupUser) => {
                  setFieldValue('roles', groupUser.roles)
                  setFieldValue('applications', groupUser.applications)
                }}
                roles={roles.filter(r => r.applicationId === a.id)}
                label={a.name + ' Roles'}
                applicationId={a.id}
              />
            ))}
          </div>
          {ContextualSubmissionButton('Update user', undefined, isValid)}
        </Form>
      )}
    </Formik>
  )
}

export default UserRoleSelector