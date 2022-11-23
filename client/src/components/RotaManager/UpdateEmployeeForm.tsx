import { useState } from "react";
import { Formik, Form } from "formik";
import { Transition } from "@headlessui/react";
import { useEmployeeService } from "../../hooks";
import { IContextualFormProps, IEmployee } from "../../models";
import { CustomDate, FormSection, FormikInput, ListboxSelector } from "../Common";
import { generateColour } from "../../services";

interface IUpdateEmployeeFormProps {
  employee: IEmployee
}

const UpdateEmployeeForm = ({ employee, ContextualSubmissionButton }: IUpdateEmployeeFormProps & IContextualFormProps) => {

  const [showAddress, setShowAddress] = useState(false);
  const [showRequirements, setShowRequirements] = useState(false);

  const toggleShowAddress = () => setShowAddress(!showAddress);
  const toggleShowRequirements = () => setShowRequirements(!showRequirements);

  const { employees, getEmployee, updateEmployee } = useEmployeeService()

  const roles = [
    { id: '9c0c9ef7-010d-4776-a488-876b2245ff2e', name: 'Manager' },
    { id: '513646d6-a28d-436b-9f8a-350f4bc0f76e', name: 'Staff' },
  ]

  return (
    <Formik
      initialValues={{
        reportsTo: employee.reportsTo || '',
        role: employee.role || '',
        firstName: employee.firstName || '',
        lastName: employee.lastName || '',
        middleNames: employee.middleNames || '',
        firstLine: employee.firstLine || '',
        secondLine: employee.secondLine || '',
        thirdLine: employee.thirdLine || '',
        city: employee.city || '',
        country: employee.country || '',
        postCode: employee.postCode || '',
        primaryPhoneNumber: employee.primaryPhoneNumber || '',
        primaryEmailAddress: employee.primaryEmailAddress || '',
        birthDate: employee.birthDate,
        startDate: employee.startDate || new Date().toISOString().split('T')[0] || null,
        minHours: employee.minHours || '',
        maxHours: employee.maxHours || '',
        unavailableDays: employee.unavailableDays || [],
        colour: employee.colour || generateColour()
      }}
      onSubmit={(values) => {
        updateEmployee(employee.id, values);
      }}
    >
      {({ errors, touched, values, setFieldValue, isValid }) => (
        <Form>
          <div className="flex flex-grow flex-col space-y-6 content-end">
            <FormSection title="Job description">
              <div className="grid grid-cols-2 gap-2">
                <ListboxSelector<string>
                  label="Employee Role"
                  showLabel
                  items={roles.map(r => ({ value: r.id, label: r.name }))}
                  initialSelected={values.role ? (r => r ? { value: r.id, label: r.name } : null)(roles.find(r => r.id === values.role)) : null}
                  onUpdate={(selected) => setFieldValue('role', selected.value)}
                />
                <ListboxSelector<string>
                  label="Reports To"
                  showLabel
                  items={employees.map(e => ({ label: `${e.firstName} ${e.lastName}`, value: e.id }))}
                  initialSelected={(e => e ? { value: e.id, label: `${e.firstName} ${e.lastName}` } : null)(getEmployee(values.reportsTo)) ?? null}
                  onUpdate={(selected) => setFieldValue('role', selected.value)}
                />
              </div>
            </FormSection>
            <FormSection title="Employee information">
              <div className="grid grid-cols-1 gap-4">
                <div className="grid grid-cols-3 gap-2">
                  <FormikInput name="firstName" label="First name" errors={errors.firstName} touched={touched.firstName} />
                  <FormikInput name="middleNames" label="Middle Names" errors={errors.middleNames} touched={touched.middleNames} />
                  <FormikInput name="lastName" label="Last name" errors={errors.lastName} touched={touched.lastName} />
                </div>
                <div className="grid grid-cols-2 gap-2">
                  <FormikInput name="primaryEmailAddress" label="Email" errors={errors.primaryEmailAddress} touched={touched.primaryEmailAddress} />
                  <FormikInput name="primaryPhoneNumber" label="Phone number" errors={errors.primaryPhoneNumber} touched={touched.primaryPhoneNumber} />
                  <FormikInput type="date" name="birthDate" label="Date of Birth" component={CustomDate} errors={errors.birthDate} touched={touched.birthDate} />
                  <FormikInput type="date" name="startDate" label="Start date" component={CustomDate} errors={errors.startDate} touched={touched.startDate} />
                </div>
              </div>
            </FormSection>
            <FormSection title="Employee address" showExpander expanded={showAddress} expanderAction={toggleShowAddress}>
              <Transition
                show={showAddress}
                enter="transition ease-out duration-200"
                enterFrom="transform opacity-0 scale-y-0"
                enterTo="transform opacity-100 scale-y-100"
                leave="transition ease-in duration-200"
                leaveFrom="transform opacity-100 scale-y-100"
                leaveTo="transform opacity-0 scale-y-0"
                className="origin-top"
              >
                <FormikInput name="firstLine" label="Address Line 1" errors={errors.firstLine} touched={touched.firstLine} />
                <FormikInput name="secondLine" label="Address Line 2" errors={errors.secondLine} touched={touched.secondLine} />
                <FormikInput name="thirdLine" label="Address Line 3" errors={errors.thirdLine} touched={touched.thirdLine} />
                <div className="grid grid-cols-3 gap-2">
                  <FormikInput name="city" label="City" errors={errors.city} touched={touched.city} />
                  <FormikInput name="country" label="Country" errors={errors.country} touched={touched.country} />
                  <FormikInput name="postCode" label="Post Code" errors={errors.postCode} touched={touched.postCode} />
                </div>
              </Transition>
            </FormSection>
            <FormSection title="Scheduling requirements" showExpander expanded={showRequirements} expanderAction={toggleShowRequirements}>
              <Transition
                show={showRequirements}
                enter="transition ease-out duration-200"
                enterFrom="transform opacity-0 scale-y-0"
                enterTo="transform opacity-100 scale-y-100"
                leave="transition ease-in duration-200"
                leaveFrom="transform opacity-100 scale-y-100"
                leaveTo="transform opacity-0 scale-y-0"
                className="origin-top flex flex-col space-y-2"
              >
                <div className="grid grid-cols-2 gap-2">
                  <FormikInput name="minHours" label="Minimum hours" errors={errors.minHours} touched={touched.minHours} />
                  <FormikInput name="maxHours" label="Maximum hours" errors={errors.maxHours} touched={touched.maxHours} />
                </div>
              </Transition>
            </FormSection>
          </div>
          {ContextualSubmissionButton('Update employee', undefined, isValid)}
        </Form>
      )}
    </Formik>
  )
}

export default UpdateEmployeeForm;