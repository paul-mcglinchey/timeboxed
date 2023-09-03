import { useApplicationService, useGroupUserService } from "../../hooks"
import { IApplication, IContextualFormProps, IGroup, IGroupUser } from "../../models"
import { Formik } from "formik"

import UserRoleGroup from "./UserRoleGroup"
import { useContext, useState } from "react"
import { MetaInfoContext } from "../../contexts"
import { IApiError } from "../../models/error.model"
import { FormikForm } from "../Common/FormikForm"

interface IUserRoleSelectorProps {
  group: IGroup
  groupUser: IGroupUser
}

const UserRoleSelector = ({ group, groupUser, ContextualSubmissionButton }: IUserRoleSelectorProps & IContextualFormProps) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()
  
  const { roles } = useContext(MetaInfoContext)
  const { getApplication } = useApplicationService()
  const { updateGroupUser } = useGroupUserService(setIsLoading, setError)

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
        <FormikForm error={error}>
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
          {ContextualSubmissionButton('Update user', undefined, isValid, isLoading)}
        </FormikForm>
      )}
    </Formik>
  )
}

export default UserRoleSelector