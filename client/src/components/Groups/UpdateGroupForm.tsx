import { Formik } from "formik";
import { useGroupService } from "../../hooks";
import { IContextualFormProps, IGroup } from "../../models";
import { generateColour } from "../../services";
import { groupValidationSchema } from "../../schema";
import { ColourPicker, FormSection, Modal, FormikInput, Button, SpinnerLoader } from "../Common";
import { useEffect, useState } from "react";
import { FormikForm } from "../Common/FormikForm";
import { IApiError } from "../../models/error.model";

import ApplicationMultiSelector from "./ApplicationMultiSelector";
import UserInvites from "./UserInvites";
import GroupUserEntry from "./GroupUserEntry";

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
          {({ errors, touched, values, setFieldValue, isValid }) => (
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
                    <GroupUserEntry group={group} gu={gu} key={gu.userId} />
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
              {ContextualSubmissionButton('Update group', undefined, isValid, isLoading)}
            </FormikForm>
          )}
        </Formik >
      ) : (
        isLoading && <SpinnerLoader />
      )}
    </>
  )
}

export default UpdateGroupForm;