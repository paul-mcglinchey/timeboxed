import { Form, Formik } from "formik";
import { useGroupService, useUserService } from "../../hooks";
import { IContextualFormProps, IGroup, IGroupUser, IUser } from "../../models";
import { combineClassNames, generateColour } from "../../services";
import { groupValidationSchema } from "../../schema";
import { ColourPicker, FormSection, Modal, FormikInput, Button, SpinnerLoader } from "../Common";
import { ApplicationMultiSelector, UserInvites, UserRoleSelector } from '.'
import { useState } from "react";
import { PencilIcon } from "@heroicons/react/solid";
import { Role } from "../../enums";

interface IUpdateGroupFormProps {
  group: IGroup
}

const UpdateGroupForm = ({ group, ContextualSubmissionButton }: IUpdateGroupFormProps & IContextualFormProps) => {

  const [editGroupUsersOpen, setEditGroupUsersOpen] = useState<boolean>(false)
  const toggleEditGroupUsersOpen = () => setEditGroupUsersOpen(!editGroupUsersOpen)

  const [inviteUserOpen, setInviteUserOpen] = useState<boolean>(false)
  const toggleInviteUserOpen = () => setInviteUserOpen(!inviteUserOpen)

  const { updateGroup } = useGroupService()
  const { getUser, userHasRole, isLoading } = useUserService()

  return (
    <Formik
      initialValues={{
        name: group.name || '',
        description: group.description || '',
        applications: group.applications || [],
        colour: group.colour || generateColour()
      }}
      validationSchema={groupValidationSchema}
      onSubmit={(values) => {
        updateGroup(values, group.id)
      }}
    >
      {({ errors, touched, values, setFieldValue, isValid }) => (
        <Form>
          <FormSection title="Details">
            <div className="flex items-end space-x-2">
              <FormikInput name="name" label="Groupname" errors={errors.name} touched={touched.name} classes="flex flex-grow" />
              <ColourPicker square colour={values.colour} setColour={(pc) => setFieldValue('colour', pc)} />
            </div>
            <FormikInput as="textarea" name="description" label="Description" errors={errors.description} touched={touched.description} />
          </FormSection>
          <FormSection title="Group Applications" classes="mb-6">
            <ApplicationMultiSelector
              formValues={values.applications}
              setFieldValue={(a) => setFieldValue('applications', a)}
            />
          </FormSection>
          <FormSection title="Users" titleActionComponent={<Button action={toggleInviteUserOpen} content="Invite" type="button" />}>
            {isLoading ? (
              <SpinnerLoader />
            ) : (
              <>
                {group.groupUsers.filter((gu): gu is IGroupUser => !!getUser(gu.userId)).map(gu => (
                  <div key={gu.userId} className="flex">
                    <button type="button" onClick={toggleEditGroupUsersOpen} className="w-full grid grid-cols-8 items-center group hover:bg-gray-300 dark:hover:bg-gray-900 rounded-md p-2">
                      <UserDetails user={getUser(gu.userId)!} />
                      <div className="col-span-2 grid grid-cols-6 items-center">
                        <div className={combineClassNames(
                          "col-span-5 font-bold px-2 py-0.5 border border-current rounded-md",
                          gu.hasJoined ? userHasRole(group, gu.userId, Role.GroupAdmin) ? "text-orange-500" : "text-blue-500" : "text-emerald-500"
                        )}>
                          {gu.hasJoined
                            ? userHasRole(group, gu.userId, Role.GroupAdmin)
                              ? 'Admin'
                              : 'Member'
                            : 'Pending'
                          }
                        </div>
                        <div className="col-span-1 flex flex-1 justify-end">
                          <PencilIcon className="w-6 h-6 group-hover:text-blue-500 transition-colors" />
                        </div>
                      </div>
                    </button>
                    <Modal
                      title="Edit group users"
                      description="This dialog can be used to edit and update an existing user's roles within the currently selected group."
                      isOpen={editGroupUsersOpen}
                      close={toggleEditGroupUsersOpen}
                      level={2}
                    >
                      {(ConfirmationButton) => (
                        <UserRoleSelector group={group} userId={gu.userId} ContextualSubmissionButton={ConfirmationButton} />
                      )}
                    </Modal>
                  </div>
                ))}
              </>
            )}
            <Modal
              title="Invite user"
              description="This dialog can be used to invite existing users to the currently selected group."
              isOpen={inviteUserOpen}
              close={toggleInviteUserOpen}
              level={2}
            >
              {() => (
                <div>
                  <UserInvites g={group} />
                </div>
              )}
            </Modal>
          </FormSection>
          {ContextualSubmissionButton(group ? 'Update group' : 'Create group', undefined, isValid)}
        </Form>
      )}
    </Formik >
  )
}

interface IUserDetails {
  user: IUser
}

const UserDetails = ({ user }: IUserDetails) => {
  return (
    <div className="col-span-6 flex flex-col text-left">
      <span className="uppercase font-bold text-lg tracking-wider">{user.username}</span>
      <span className="text-sm tracking-wide dark:text-gray-500 text-gray-500">{user.email}</span>
    </div>
  )
}

export default UpdateGroupForm;