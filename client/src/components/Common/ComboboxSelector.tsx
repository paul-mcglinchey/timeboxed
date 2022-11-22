import { useEffect, useState } from "react"
import { Combobox } from "@headlessui/react"
import { CheckIcon, SelectorIcon } from "@heroicons/react/solid"
import { combineClassNames } from "../../services"
import { FadeInOut } from "./Transitions"

interface IHasCustomClasses {
  classes?: string | undefined
}

interface IComboboxProps extends IHasCustomClasses {
  label: string
  showLabel?: boolean
  labelFieldName: string
  labelClasses?: string
  buttonClasses?: string
  optionsClasses?: string
  optionClasses?: string
}

interface IComboboxMultiSelectorProps<T> extends IComboboxProps {
  items: T[]
  initialSelected: T[]
  onUpdate?: (items: T[]) => void
}

export const ComboboxMultiSelector = <T extends any>({ items, initialSelected, labelFieldName, optionsClasses, onUpdate = () => { } }: IComboboxMultiSelectorProps<T>) => {

  const [selected, setSelected] = useState<T[]>(initialSelected)

  const updateSelected = (selected: T[]) => setSelected(selected) 

  useEffect(() => {
    onUpdate(selected)
  }, [selected, onUpdate])

  return (
    <Combobox value={selected} onChange={updateSelected} multiple>
      <Combobox.Input
        className="w-full border-none py-2 pl-3 pr-10 text-sm leading-5 text-gray-900 focus:ring-0"
        displayValue={(selected: T[]) => selected.map(item => item[labelFieldName as keyof T]).join(', ')}
        onChange={() => { }}
      />
      <Combobox.Button className="absolute inset-y-0 right-0 flex items-center pr-2">
        <SelectorIcon
          className="h-5 w-5 text-gray-400"
          aria-hidden="true"
        />
      </Combobox.Button>
      <FadeInOut>
        <Combobox.Options className={combineClassNames(
          "focus:outline-none absolute origin-top-right z-50 mt-1 overflow-auto rounded-md py-1 text-base ring-1 ring-black ring-opacity-5",
          "bg-gray-200 dark:bg-slate-800",
          optionsClasses
        )}>
          {items.map((item, index) => (
            <Combobox.Option
              key={index}
              className={({ active }) =>
                `relative cursor-pointer select-none py-2 pl-10 pr-4 transition-all 
                  ${active && 'text-blue-500'}`
              }
              value={item}
            >
              {({ selected }) => (
                <>
                  <span className={`block truncate font-semibold`}>
                    {item[labelFieldName as keyof T]}
                  </span>
                  {selected && (
                    <span className="absolute inset-y-0 left-0 flex items-center pl-3 text-blue-500">
                      <CheckIcon className="h-5 w-5" aria-hidden="true" />
                    </span>
                  )}
                </>
              )}
            </Combobox.Option>
          ))}
        </Combobox.Options>
      </FadeInOut>
    </Combobox>
  )
}