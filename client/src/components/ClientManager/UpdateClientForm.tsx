import { useEffect, useState } from "react";
import { Formik, Form } from "formik";
import { Transition } from "@headlessui/react";
import { generateColour } from "../../services";
import { useClientService } from "../../hooks";
import { IClient, IContextualFormProps } from "../../models";
import { CustomDate, FormikInput, FormSection } from "..";
import { updateClientValidationSchema } from "../../schema/clientValidationSchema";
import { IApiError } from "../../models/error.model";

interface IUpdateClientFormProps {
  clientId: string
  submissionAction?: () => Promise<void> | void
}

const UpdateClientForm = ({ clientId, submissionAction, ContextualSubmissionButton }: IUpdateClientFormProps & IContextualFormProps) => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()

  const [addressExpanded, setAddressExpanded] = useState(false)
  const [client, setClient] = useState<IClient | undefined>()

  const { getClient, updateClient } = useClientService(setIsLoading, setError)

  useEffect(() => {
    const _fetch = async () => {
      setClient(await getClient(clientId))
    }

    _fetch()
  }, [clientId, getClient])

  return (
    <>
      {client ? (
        <Formik
          enableReinitialize
          initialValues={{
            firstName: client.firstName || '',
            lastName: client.lastName || '',
            middleNames: client.middleNames || '',
            firstLine: client.firstLine || '',
            secondLine: client.secondLine || '',
            thirdLine: client.thirdLine || '',
            city: client.city || '',
            country: client.country || '',
            postCode: client.postCode || '',
            primaryPhoneNumber: client.primaryPhoneNumber || '',
            primaryEmailAddress: client.primaryEmailAddress || '',
            birthDate: client.birthDate || '',
            colour: client.colour || generateColour()
          }}
          validationSchema={updateClientValidationSchema}
          onSubmit={async (values) => {
            await updateClient(client.id, values)
            submissionAction && await submissionAction()
          }}
        >
          {({ errors, touched, isValid }) => (
            <Form>
              <FormSection title="Details">
                <FormikInput name="firstName" label="First name" errors={errors.firstName} touched={touched.firstName} />
                <FormikInput name="middleNames" label="Middle Names" errors={errors.middleNames} touched={errors.middleNames} />
                <FormikInput name="lastName" label="Last name" errors={errors.lastName} touched={errors.lastName} />
                <FormikInput name="primaryEmailAddress" label="Email" errors={errors.primaryEmailAddress} touched={touched.primaryEmailAddress} />
                <FormikInput name="primaryPhoneNumber" label="Phone number" errors={errors.primaryPhoneNumber} touched={touched.primaryPhoneNumber} />
                <FormikInput id="birthdate" type="date" name="birthDate" label="Date of Birth" component={CustomDate} errors={errors.birthDate} touched={touched.birthDate} />
              </FormSection>
              <FormSection title="Address" expanded={addressExpanded} expanderAction={() => setAddressExpanded(!addressExpanded)}>
                <Transition
                  show={addressExpanded}
                  enter="transition ease-out duration-200"
                  enterFrom="transform opacity-0 scale-y-0"
                  enterTo="transform opacity-100 scale-y-100"
                  leave="transition ease-in duration-200"
                  leaveFrom="transform opacity-100 scale-y-100"
                  leaveTo="transform opacity-0 scale-y-0"
                  className="origin-top"
                >
                  <FormikInput name="firstLine" label="Address Line 1" errors={errors?.firstLine} touched={touched?.firstLine} />
                  <FormikInput name="secondLine" label="Address Line 2" errors={errors?.secondLine} touched={touched?.secondLine} />
                  <FormikInput name="thirdLine" label="Address Line 3" errors={errors?.thirdLine} touched={touched?.thirdLine} />
                  <div className="grid grid-cols-3 gap-2">
                    <FormikInput name="city" label="City" errors={errors?.city} touched={touched?.city} />
                    <FormikInput name="country" label="Country" errors={errors?.country} touched={touched?.country} />
                    <FormikInput name="postCode" label="Post Code" errors={errors?.postCode} touched={touched?.postCode} />
                  </div>
                </Transition>
              </FormSection>
              {ContextualSubmissionButton(client ? 'Update client' : 'Add client', undefined, isValid)}
            </Form>
          )}
        </Formik>
      ) : (
        isLoading ? (
          <div>Loading...</div>
        ) : (
          error && (
            <div>{error.message}</div>
          )
        )
      )}
    </>
  )
}

export default UpdateClientForm;