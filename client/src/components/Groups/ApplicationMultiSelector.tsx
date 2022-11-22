import { useApplicationService } from "../../hooks"
import { IApplication } from "../../models"
import { MultiSelector } from "../Common"

interface IApplicationMultiSelectorProps {
  formValues: number[]
  setFieldValue: (value: (number | undefined)[]) => void
  fieldName?: string
}

const ApplicationMultiSelector = ({ formValues, setFieldValue, fieldName = "applications" }: IApplicationMultiSelectorProps) => {

  const { applications, getApplication } = useApplicationService()

  return (
    <MultiSelector<number | undefined>
      fieldName={fieldName}
      values={applications.map(a => a.id)}
      totalValuesLength={applications.length}
      toggleShowAll={() => { }}
      formValues={formValues}
      setFieldValue={(a) => setFieldValue(a)}
      render={(a) => (
        <div>
          <ApplicationSelector a={getApplication(a)} />
        </div>
      )}
    />
  )
}

const ApplicationSelector = ({ a }: { a: IApplication | undefined }) => {
  return a ? (
    <div className="flex flex-col text-left space-y-2 leading-loose">
      <div className="font-bold tracking-wider text-lg uppercase">{a.name}</div>
      <div className="text-sm">{a.description}</div>
    </div>
  ) : <></>
}

export default ApplicationMultiSelector