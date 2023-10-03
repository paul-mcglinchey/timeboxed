import { Formik } from "formik";
import { useGroupService } from "../../hooks";
import { IContextualFormProps, IGroup } from "../../models";
import { generateColour } from "../../services";
import { groupValidationSchema } from "../../schema";
import { ColourPicker, FormSection, Modal, FormikInput, Button, SpinnerLoader, FormikTextArea, Table, TableRow } from "../Common";
import { useCallback, useEffect, useState } from "react";
import { FormikForm } from "../Common/FormikForm";
import { IApiError } from "../../models/error.model";

import UserInvites from "./UserInvites";
import GroupUserEntry from "./GroupUserEntry";
import FormGrouping from "../Common/FormGrouping";

interface IUpdateGroupFormProps {
  groupId: string
}

const UpdateGroupForm = ({ groupId, ContextualSubmissionButton }: IUpdateGroupFormProps & IContextualFormProps) => {

  const [loading, setLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()
  const [group, setGroup] = useState<IGroup | undefined>()

  const [inviteUserOpen, setInviteUserOpen] = useState<boolean>(false)

  const { getGroup, updateGroup } = useGroupService(setLoading, setError)

  const fetchGroup = useCallback(async () => {
    setLoading(true)
    setGroup(await getGroup(groupId))
    setLoading(false)
  }, [groupId, getGroup])

  useEffect(() => {
    fetchGroup()
  }, [])

  return (
    <>
      {group ? (
        <Formik
          enableReinitialize
          initialValues={{
            name: group.name || '',
            description: group.description || '',
            colour: group.colour || generateColour()
          }}
          validationSchema={groupValidationSchema}
          onSubmit={(values) => {
            updateGroup({ ...group, ...values}, group.id)
          }}
        >
          {({ errors, touched, values, setFieldValue, isValid }) => (
            <FormikForm error={error}>
              <FormSection title="Details">
                <FormGrouping>
                  <div className="flex items-end space-x-2">
                    <FormikInput name="name" label="Groupname" errors={errors.name} touched={touched.name} />
                    <ColourPicker square colour={values.colour} setColour={(pc) => setFieldValue('colour', pc)} />
                  </div>
                  <FormikTextArea name="description" label="Description" errors={errors.description} touched={touched.description} />
                </FormGrouping>
              </FormSection>
              <FormSection title="Users" titleActionComponent={<Button action={() => setInviteUserOpen(true)} content="Invite" type="button" />}>
                <Table isLoading={false} compact>
                  <Table.Body>
                    {group.users
                      .map(gu => (
                        <GroupUserEntry group={group} gu={gu} key={gu.userId} />
                      ))
                    }
                  </Table.Body>
                </Table>
                <Modal
                  title="Invite user"
                  description="This dialog can be used to invite existing users to the currently selected group."
                  isOpen={inviteUserOpen}
                  close={() => setInviteUserOpen(false)}
                  level={2}
                >
                  {() => (
                    <UserInvites groupId={group.id} />
                  )}
                </Modal>
              </FormSection>
              {ContextualSubmissionButton('Update group', undefined, isValid, loading)}
            </FormikForm>
          )}
        </Formik >
      ) : (
        loading && <SpinnerLoader />
      )}
    </>
  )
}

export default UpdateGroupForm;