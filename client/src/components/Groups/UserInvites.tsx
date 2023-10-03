import { Formik } from "formik"
import { useCallback, useEffect, useState } from "react"
import { useGroupUserService } from "../../hooks"
import { IGroupUser } from "../../models"
import { IApiError } from "../../models/error.model"
import userInviteValidationSchema from "../../schema/userInviteValidationSchema"
import { Button, FormikInput, SpinnerIcon } from "../Common"
import { FormikForm } from "../Common/FormikForm"
import FormGrouping from "../Common/FormGrouping"

interface IUserInvitesProps {
  groupId: string
}

const UserInvites = ({ groupId }: IUserInvitesProps) => {

  const [loading, setLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()
  const [users, setUsers] = useState<IGroupUser[]>([])
  const [count, setCount] = useState<number>(0)

  const { getGroupUsers, inviteGroupUser, uninviteGroupUser } = useGroupUserService(setLoading, setError)

  const fetchUsers = useCallback(async () => {
    const res = await getGroupUsers(groupId, [{ name: 'hasJoined', value: 'false', label: null }])
    setUsers(res.items)
    setCount(res.count)
  }, [getGroupUsers])

  useEffect(() => {
    fetchUsers()
  }, [fetchUsers])

  return (
    <>
      <Formik
        initialValues={{
          usernameOrEmail: ''
        }}
        onSubmit={(values) => {
          inviteGroupUser(groupId, values)
        }}
        validationSchema={userInviteValidationSchema}
      >
        {({ errors, touched }) => (
          <FormikForm error={error}>
            <FormGrouping>
              <div className="flex items-end gap-2">
                <FormikInput name="usernameOrEmail" label="Username or Email" errors={errors.usernameOrEmail} touched={touched.usernameOrEmail} classes="flex flex-grow" />
                <div className="pb-1.5">
                  <Button content="Invite" buttonType="Tertiary" Icon={loading ? SpinnerIcon : null} />
                </div>
              </div>
            </FormGrouping>
          </FormikForm>
        )}
      </Formik>
      {count > 0 && users.map(gu => (
        <div className="mt-4 pt-4 border-t border-gray-500">
          <div key={gu.userId} className="flex justify-between items-center">
            <div className="flex flex-col text-left">
              <span className="uppercase font-bold text-lg tracking-wider">{gu.username}</span>
              <span className="text-sm tracking-wide dark:text-gray-500 text-gray-500">{gu.email}</span>
            </div>
            <Button type='button' buttonType="Cancel" content="Revoke Invite" action={() => uninviteGroupUser(gu.groupId, gu.userId)} />
          </div>
        </div>
      ))}
    </>
  )
}

export default UserInvites