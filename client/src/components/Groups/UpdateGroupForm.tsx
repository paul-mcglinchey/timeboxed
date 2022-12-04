import { Formik } from "formik";
import { useGroupService, useUserService } from "../../hooks";
import { IContextualFormProps, IGroup, IMappableGroupUser } from "../../models";
import { combineClassNames, generateColour } from "../../services";
import { groupValidationSchema } from "../../schema";
import { ColourPicker, FormSection, Modal, FormikInput, Button, SpinnerLoader } from "../Common";
import { useState } from "react";
import { PencilIcon } from "@heroicons/react/solid";
import { Role } from "../../enums";
import { FormikForm } from "../Common/FormikForm";

import ApplicationMultiSelector from "./ApplicationMultiSelector";
import UserInvites from "./UserInvites";
import UserRoleSelector from "./UserRoleSelector";
import { IApiError } from "../../models/error.model";

interface IUpdateGroupFormProps {
  group: IGroup
}

const UpdateGroupForm = ({ group, ContextualSubmissionButton }: IUpdateGroupFormProps & IContextualFormProps) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()

  const [inviteUserOpen, setInviteUserOpen] = useState<boolean>(false)
  const toggleInviteUserOpen = () => setInviteUserOpen(!inviteUserOpen)

  const { updateGroup } = useGroupService(setIsLoading, setError)
  const { getUser, isLoading: isUsersLoading } = useUserService()

  return (
    <Formik
      enableReinitialize
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
      {({ errors, touched, values, setFieldValue, isValid, dirty }) => (
        <FormikForm error={error}>
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
            {isUsersLoading ? (
              <SpinnerLoader />
            ) : (
              <>
                {group.groupUsers
                  .map<IMappableGroupUser>(gu => ({ gu: gu, user: getUser(gu.userId) }))
                  .filter(gu => !!gu.user)
                  .map(u => (
                    <GroupUserRow u={u} key={u.gu.userId}/>
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
          {ContextualSubmissionButton('Update group', undefined, isValid, dirty, isLoading)}
        </FormikForm>
      )}
    </Formik >
  )
}

interface IGroupUserRowProps {
  u: IMappableGroupUser
}

const GroupUserRow = ({ u }: IGroupUserRowProps) => {

  const [editGroupUsersOpen, setEditGroupUsersOpen] = useState<boolean>(false)
  const toggleEditGroupUsersOpen = () => setEditGroupUsersOpen(!editGroupUsersOpen)

  const { userHasRole } = useUserService()

  return (
    <div key={u.gu.userId} className="flex">
      <button type="button" onClick={toggleEditGroupUsersOpen} className="w-full grid grid-cols-8 items-center group hover:bg-gray-300 dark:hover:bg-gray-900 rounded-md p-2">
        <div className="col-span-6 flex flex-col text-left">
          <span className="uppercase font-bold text-lg tracking-wider">{u.user?.username}</span>
          <span className="text-sm tracking-wide dark:text-gray-500 text-gray-500">{u.user?.email}</span>
        </div>
        <div className="col-span-2 grid grid-cols-6 items-center">
          <div className={combineClassNames(
            "col-span-5 font-bold px-2 py-0.5 border border-current rounded-md",
            u.gu.hasJoined ? userHasRole(u.gu, Role.GroupAdmin) ? "text-orange-500" : "text-blue-500" : "text-emerald-500"
          )}>
            {u.gu.hasJoined
              ? userHasRole(u.gu, Role.GroupAdmin)
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
          <UserRoleSelector groupUser={u.gu} ContextualSubmissionButton={ConfirmationButton} />
        )}
      </Modal>
    </div>
  )
}

export default UpdateGroupForm;