import { CheckIcon } from "@heroicons/react/solid"
import { FieldArray } from "formik"
import { combineClassNames } from "../../services"

interface IListSelectorProps<TValue> {
  fieldName: string,
  values: TValue[],
  totalValuesLength: number,
  toggleShowAll: () => void,
  formValues: TValue[],
  setFieldValue: (value: TValue[]) => void
  render: (value: TValue) => JSX.Element
  itemStyles?: (selected: boolean) => string
}

const MultiSelector = <TValue extends unknown>({ fieldName, values, totalValuesLength, toggleShowAll, formValues, setFieldValue, render, itemStyles }: IListSelectorProps<TValue>) => {

  const toggleValue = (value: TValue): TValue[] => {
    if (formValues.includes(value)) {
      formValues = formValues.filter((v: TValue) => v !== value)
    } else {
      formValues.push(value)
    }

    return formValues;
  }

  return (
    <FieldArray
      name={fieldName}
      render={() => (
        <>
          <div className="flex flex-col space-y-3">
            {values && values.map((value: TValue, index: number) => (
              <button
                type="button"
                key={index}
                className={
                  (itemStyles && itemStyles(formValues.includes(value))) || combineClassNames(
                    "flex flex-grow p-4 transition-colors justify-between items-center border-2 rounded",
                    formValues.includes(value) ? 'text-blue-500 border-blue-500' : 'bg-white dark:bg-gray-900 border-transparent'
                  )}
                onClick={() => setFieldValue(toggleValue(value))}
              >
                {render(value)}
                {formValues.includes(value) && <CheckIcon className="w-6 h-6" />}
              </button>
            ))}
          </div>
          <div className="text-right tracking-wide font-semibold mt-2">
            Showing {values.length} of {totalValuesLength}
            {values.length < totalValuesLength && (
              <button type="button" className="tracking-wide font-semibold" onClick={() => toggleShowAll()}> - show all</button>
            )}
          </div>
        </>
      )}
    />
  )
}

export default MultiSelector