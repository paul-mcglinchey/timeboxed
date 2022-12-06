import { Formik } from "formik"
import { useState } from "react"
import { useGroupUserService } from "../../hooks"
import { IGroup } from "../../models"
import { IApiError } from "../../models/error.model"
import userInviteValidationSchema from "../../schema/userInviteValidationSchema"
import { Button, FormikInput, SpinnerIcon } from "../Common"
import { FormikForm } from "../Common/FormikForm"

interface IUserInvitesProps {
  g: IGroup
}

const UserInvites = ({ g }: IUserInvitesProps) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()

  const { inviteGroupUser, uninviteGroupUser } = useGroupUserService(setIsLoading, setError)

  return (
    <>
      <Formik
        initialValues={{
          usernameOrEmail: ''
        }}
        onSubmit={(values) => {
          inviteGroupUser(g.id, values)
        }}
        validationSchema={userInviteValidationSchema}
      >
        {({ errors, touched }) => (
          <FormikForm error={error}>
            <div className="flex items-end space-x-2">
              <FormikInput name="usernameOrEmail" label="Username or Email" errors={errors.usernameOrEmail} touched={touched.usernameOrEmail} classes="flex flex-grow" />
              <div className="pb-1.5">
                <Button content="Invite" buttonType="Tertiary" Icon={isLoading ? SpinnerIcon : null} />
              </div>
            </div>
          </FormikForm>
        )}
      </Formik>
      <div className="mt-4 pt-4 border-t border-gray-500">
        {g.users
          .filter(gu => !gu.hasJoined)
          .map(gu => (
            <div key={gu.userId} className="flex justify-between items-center">
              <div className="flex flex-col text-left">
                <span className="uppercase font-bold text-lg tracking-wider">{gu.username}</span>
                <span className="text-sm tracking-wide dark:text-gray-500 text-gray-500">{gu.email}</span>
              </div>
              <Button type='button' buttonType="Cancel" content="Revoke Invite" action={() => uninviteGroupUser(gu.groupId, gu.userId)} />
            </div>
          ))}
      </div>
    </>
  )
}

export default UserInvites