import { Form, Formik } from "formik"
import { useGroupUserService, useUserService } from "../../hooks"
import { IGroup, IMappableGroupUser } from "../../models"
import { Button, FormikInput, SpinnerIcon } from "../Common"

interface IUserInvitesProps {
  g: IGroup
}

const UserInvites = ({ g }: IUserInvitesProps) => {

  const { inviteGroupUser, uninviteGroupUser } = useGroupUserService()
  const { getUser } = useUserService()

  return (
    <>
      <Formik
        initialValues={{
          usernameOrEmail: ''
        }}
        onSubmit={(values) => {
          inviteGroupUser(g.id, values)
        }}
      >
        {({ errors, touched, isSubmitting }) => (
          <Form>
            <div className="flex items-end space-x-2">
              <FormikInput name="usernameOrEmail" label="Username or Email" errors={errors.usernameOrEmail} touched={touched.usernameOrEmail} classes="flex flex-grow" />
              <div className="pb-1.5">
                <Button content="Invite" buttonType="Tertiary" Icon={isSubmitting ? SpinnerIcon : null} />
              </div>
            </div>
          </Form>
        )}
      </Formik>
      <div className="mt-4 pt-4 border-t border-gray-500">
        {g.groupUsers
          .filter(gu => !gu.hasJoined)
          .map<IMappableGroupUser>(gu => ({ gu: gu, user: getUser(gu.userId) }))
          .map(u => (
            <div key={u.gu.userId} className="flex justify-between items-center">
              <div className="flex flex-col text-left">
                <span className="uppercase font-bold text-lg tracking-wider">{u.user?.username}</span>
                <span className="text-sm tracking-wide dark:text-gray-500 text-gray-500">{u.user?.email}</span>
              </div>
              <Button type='button' buttonType="Cancel" content="Revoke Invite" action={() => uninviteGroupUser(u.gu.groupId, u.gu.userId)} />
            </div>
        ))}
      </div>
    </>
  )
}

export default UserInvites