import { Form, Formik } from "formik";
import { useGroupService, useUserService } from "../../hooks";
import { IContextualFormProps, IGroup, IUser } from "../../models";
import { generateColour } from "../../services";
import { groupValidationSchema } from "../../schema";
import { ColourPicker, FormSection, Modal, FormikInput, Button, SpinnerLoader } from "../Common";
import { ApplicationMultiSelector, UserInvites, UserRoleSelector } from '.'
import { useState } from "react";
import { PencilIcon } from "@heroicons/react/solid";

interface IUpdateGroupFormProps {
  group: IGroup
}

const UpdateGroupForm = ({ group, ContextualSubmissionButton }: IUpdateGroupFormProps & IContextualFormProps) => {

  const [editGroupUsersOpen, setEditGroupUsersOpen] = useState<boolean>(false);
  const [inviteUserOpen, setInviteUserOpen] = useState<boolean>(false)

  const { updateGroup } = useGroupService()
  const { getUser, isLoading } = useUserService()

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
          <FormSection title="Users" titleActionComponent={<Button action={() => setInviteUserOpen(true)} content="Invite" type="button" />}>
            {isLoading ? (
              <SpinnerLoader />
            ) : (
              <>
                {group.groupUsers.map(gu => getUser(gu.userId)).filter((u): u is IUser => !!u).map((u, j) => (
                  <div key={j} className="flex">
                    <button type="button" onClick={() => setEditGroupUsersOpen(true)} className="flex flex-1 justify-between items-center group" key={j}>
                      <div className="flex flex-col">
                        <span className="uppercase font-bold text-lg text-left tracking-wider">{u.username}</span>
                        <span className="text-sm tracking-wide dark:text-gray-500 text-gray-500">{u.email}</span>
                      </div>
                      <div className="flex items-center pr-2">
                        <PencilIcon className="w-6 h-6 group-hover:text-blue-500 transition-colors" />
                      </div>
                    </button>
                    <Modal
                      title="Edit group users"
                      description="This dialog can be used to edit and update an existing user's roles within the currently selected group."
                      isOpen={editGroupUsersOpen}
                      close={() => setEditGroupUsersOpen(false)}
                      level={2}
                    >
                      {(ConfirmationButton) => (
                        <UserRoleSelector group={group} userId={u.id} ContextualSubmissionButton={ConfirmationButton} />
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
              close={() => setInviteUserOpen(false)}
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

export default UpdateGroupForm;