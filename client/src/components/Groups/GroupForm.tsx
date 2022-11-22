import { Form, Formik } from "formik";
import { useGroupService, useUserService } from "../../hooks";
import { IContextualFormProps, IGroup, IUser } from "../../models";
import { generateColour } from "../../services";
import { groupValidationSchema } from "../../schema";
import { ColourPicker, FormSection, InlineButton, Modal, StyledField } from "../Common";
import { ApplicationMultiSelector, UserRoleSelector } from '.'
import { useState } from "react";

interface IGroupFormProps {
  group?: IGroup | undefined
}

const GroupForm = ({ group, ContextualSubmissionButton }: IGroupFormProps & IContextualFormProps) => {

  const [editGroupUsersOpen, setEditGroupUsersOpen] = useState<boolean>(false);

  const { addGroup, updateGroup } = useGroupService()
  const { getUser } = useUserService()

  return (
    <Formik
      initialValues={{
        name: group?.name || '',
        description: group?.description || '',
        applications: group?.applications || [],
        colour: group?.colour || generateColour()
      }}
      validationSchema={groupValidationSchema}
      onSubmit={(values) => {
        group?.id ? updateGroup(values, group?.id) : addGroup(values)
      }}
    >
      {({ errors, touched, values, setFieldValue, isValid }) => (
        <Form className="flex flex-1 flex-col space-y-8">
          <div className="flex flex-col space-y-4">
            <div className="flex flex-col space-y-4">
              <div className="flex space-x-4">
                <StyledField name="name" label="Groupname" errors={errors.name} touched={touched.name} />
                <div className="self-end mb-1">
                  <ColourPicker square colour={values.colour} setColour={(pc) => setFieldValue('colour', pc)} />
                </div>
              </div>
              <StyledField as="textarea" name="description" label="Description" errors={errors.description} touched={touched.description} />
            </div>
          </div>
          <FormSection title="Group Applications">
            <ApplicationMultiSelector
              formValues={values.applications}
              setFieldValue={(a) => setFieldValue('applications', a)}
            />
          </FormSection>
          {group && (
            <FormSection title="Users">
              {group.groupUsers.map(gu => getUser(gu.userId)).filter((u): u is IUser => !!u).map((u, j) => (
                <div className="flex justify-between" key={j}>
                  <div className="flex flex-col">
                    <span className="uppercase font-bold text-lg tracking-wider">
                      {u.username}
                    </span>
                    <span className="text-sm tracking-wide dark:text-gray-500 text-gray-500">{u?.email}</span>
                  </div>
                  <div>
                    <InlineButton color="text-blue-500" action={() => setEditGroupUsersOpen(true)}>Edit</InlineButton>
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
                </div>
              ))}
            </FormSection>
          )}
          {ContextualSubmissionButton(group ? 'Update group' : 'Create group', undefined, isValid)}
        </Form>
      )}
    </Formik>
  )
}

export default GroupForm;