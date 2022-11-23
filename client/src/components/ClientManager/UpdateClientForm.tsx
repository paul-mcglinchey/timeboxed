import { useCallback, useEffect, useState } from "react";
import { Formik, Form } from "formik";
import { Transition } from "@headlessui/react";
import { SelectorIcon } from "@heroicons/react/solid";
import { generateColour } from "../../services";
import { useClientService } from "../../hooks";
import { IClient, IContextualFormProps } from "../../models";
import { CustomDate, StyledField, FormSection } from "..";
import { updateClientValidationSchema } from "../../schema/clientValidationSchema";

interface IUpdateClientFormProps {
  clientId: string
}

const UpdateClientForm = ({ clientId, ContextualSubmissionButton }: IUpdateClientFormProps & IContextualFormProps) => {

  const [middleNamesExpanded, setMiddleNamesExpanded] = useState(false)
  const [addressExpanded, setAddressExpanded] = useState(false)
  const [client, setClient] = useState<IClient | undefined>()

  const { getClient, updateClient, isLoading, error } = useClientService()

  const _fetchClient = useCallback(async () => {
    setClient(await getClient(clientId))
  }, [setClient, clientId])

  useEffect(() => {
    _fetchClient()
  }, [_fetchClient])

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
          onSubmit={(values) => {
            updateClient(client.id, values)
          }}
        >
          {({ errors, touched, isValid }) => (
            <Form className="flex flex-grow flex-col space-y-2 content-end">
              <div className="flex flex-col md:flex-row md:space-x-2 space-x-0 space-y-1 md:space-y-0">
                <StyledField name="firstName" label="First name" errors={errors.firstName} touched={touched.firstName} />
                {middleNamesExpanded ? (
                  <StyledField name="middleNames" label="Middle Names" errors={errors.middleNames} touched={errors.middleNames} />
                ) : (
                  <div className="relative flex md:block justify-center">
                    <button className="relative" onClick={() => setMiddleNamesExpanded(!middleNamesExpanded)}>
                      <SelectorIcon className="md:h-6 md:w-6 h-10 w-10 transform md:rotate-90 text-blue-500 hover:scale-110 transition-all" />
                    </button>
                  </div>
                )}
                <StyledField name="lastName" label="Last name" errors={errors.lastName} touched={errors.lastName} />
              </div>
              <StyledField name="primaryEmailAddress" label="Email" errors={errors.primaryEmailAddress} touched={touched.primaryEmailAddress} />
              <StyledField name="primaryPhoneNumber" label="Phone number" errors={errors.primaryPhoneNumber} touched={touched.primaryPhoneNumber} />
              <StyledField type="date" name="birthDate" label="Date of Birth" component={CustomDate} errors={errors.birthDate} touched={touched.birthDate} />
              <FormSection title="Address" state={addressExpanded} setState={setAddressExpanded}>
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
                  <div className="flex flex-col space-y-2">
                    <div className="flex flex-col space-y-2">
                      <StyledField name="firstLine" label="Address Line 1" errors={errors?.firstLine} touched={touched?.firstLine} />
                      <StyledField name="secondLine" label="Address Line 2" errors={errors?.secondLine} touched={touched?.secondLine} />
                      <StyledField name="thirdLine" label="Address Line 3" errors={errors?.thirdLine} touched={touched?.thirdLine} />
                    </div>
                    <div className="flex flex-1 md:flex-row flex-col md:space-x-2 space-x-0 space-y-2 md:space-y-0">
                      <div className="md:max-w-1/5">
                        <StyledField autoComplete="false" name="city" label="City" errors={errors?.city} touched={touched?.city} />
                      </div>
                      <div className="relative">
                        <StyledField name="country" label="Country" errors={errors?.country} touched={touched?.country} />
                      </div>
                      <StyledField name="postCode" label="Post Code" errors={errors?.postCode} touched={touched?.postCode} />
                    </div>
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
            <div>{error}</div>
          )
        )
      )}
    </>
  )
}

export default UpdateClientForm;