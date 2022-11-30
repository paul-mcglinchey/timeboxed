import { Form, Formik } from "formik"
import { useGroupUserService } from "../../hooks"
import { IGroup } from "../../models"
import { Button, FormikInput, SpinnerIcon } from "../Common"

interface IUserInvitesProps {
  g: IGroup
}

const UserInvites = ({ g }: IUserInvitesProps) => {

  const { inviteGroupUser } = useGroupUserService()

  return (
    <Formik
      initialValues={{
        usernameOrEmail: ''
      }}
      onSubmit={(values, { setSubmitting }) => {
        console.log(values)

        inviteGroupUser(g.id, values)

        setSubmitting(false)
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
  )
}

export default UserInvites