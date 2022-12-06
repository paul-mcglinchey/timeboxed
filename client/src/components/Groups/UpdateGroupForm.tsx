import { Formik } from "formik";
import { useGroupService } from "../../hooks";
import { IContextualFormProps, IGroup, IGroupUser } from "../../models";
import { combineClassNames, generateColour } from "../../services";
import { groupValidationSchema } from "../../schema";
import { ColourPicker, FormSection, Modal, FormikInput, Button, SpinnerLoader } from "../Common";
import { useContext, useEffect, useState } from "react";
import { PencilIcon } from "@heroicons/react/solid";
import { Role } from "../../enums";
import { FormikForm } from "../Common/FormikForm";

import ApplicationMultiSelector from "./ApplicationMultiSelector";
import UserInvites from "./UserInvites";
import UserRoleSelector from "./UserRoleSelector";
import { IApiError } from "../../models/error.model";
import { GroupContext } from "../../contexts";

interface IUpdateGroupFormProps {
  groupId: string
}

const UpdateGroupForm = ({ groupId, ContextualSubmissionButton }: IUpdateGroupFormProps & IContextualFormProps) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()
  const [group, setGroup] = useState<IGroup | undefined>()

  const [inviteUserOpen, setInviteUserOpen] = useState<boolean>(false)
  const toggleInviteUserOpen = () => setInviteUserOpen(!inviteUserOpen)

  const { getGroup, updateGroup } = useGroupService(setIsLoading, setError)

  useEffect(() => {
    const _fetch = async () => setGroup(await getGroup(groupId))

    _fetch()
  }, [groupId, getGroup])

  return (
    <>
      {group ? (
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
                {group.users
                  .map(gu => (
                    <GroupUserRow group={group} gu={gu} key={gu.userId} />
                  ))
                }
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
      ) : (
        isLoading && <SpinnerLoader />
      )}
    </>
  )
}

interface IGroupUserRowProps {
  group: IGroup
  gu: IGroupUser
}

const GroupUserRow = ({ group, gu }: IGroupUserRowProps) => {

  const [editGroupUsersOpen, setEditGroupUsersOpen] = useState<boolean>(false)
  const toggleEditGroupUsersOpen = () => setEditGroupUsersOpen(!editGroupUsersOpen)

  const { userHasRole } = useContext(GroupContext)

  return (
    <div className="flex">
      <button type="button" onClick={toggleEditGroupUsersOpen} className="w-full grid grid-cols-8 items-center group hover:bg-gray-300 dark:hover:bg-gray-900 rounded-md p-2">
        <div className="col-span-6 flex flex-col text-left">
          {console.log(gu)}
          <span className="uppercase font-bold text-lg tracking-wider">{gu.username}</span>
          <span className="text-sm tracking-wide dark:text-gray-500 text-gray-500">{gu.email}</span>
        </div>
        <div className="col-span-2 grid grid-cols-6 items-center">
          <div className={combineClassNames(
            "col-span-5 font-bold px-2 py-0.5 border border-current rounded-md",
            gu.hasJoined ? userHasRole(gu.groupId, gu.userId, Role.GroupAdmin) ? "text-orange-500" : "text-blue-500" : "text-emerald-500"
          )}>
            {gu.hasJoined
              ? userHasRole(gu.groupId, gu.userId, Role.GroupAdmin)
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
          <UserRoleSelector group={group} groupUser={gu} ContextualSubmissionButton={ConfirmationButton} />
        )}
      </Modal>
    </div>
  )
}

export default UpdateGroupForm;