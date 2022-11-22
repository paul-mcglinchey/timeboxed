import { forwardRef, LegacyRef, useEffect, useState } from "react"
import { Listbox } from "@headlessui/react"
import { CheckIcon, SelectorIcon } from "@heroicons/react/solid"
import { combineClassNames } from "../../services"
import { FadeInOut } from "./Transitions"
import { IChildrenProps, IListboxOption } from "../../models"

interface IHasCustomClasses {
  classes?: string | undefined
}

interface IListboxProps<TValue> extends IHasCustomClasses {
  items: IListboxOption<TValue>[]
  label: string
  showLabel?: boolean
  labelClasses?: string
  buttonClasses?: string
  optionsClasses?: string
  optionClasses?: string
}

interface IListboxSelectorProps<TValue> extends IListboxProps<TValue> {
  initialSelected: IListboxOption<TValue> | null
  onUpdate?: (item: IListboxOption<TValue>) => void
}

interface IListboxMultiSelectorProps<TValue> extends IListboxProps<TValue> {
  initialSelected: IListboxOption<TValue>[]
  onUpdate?: (values: IListboxOption<TValue>[]) => void
}

interface IListboxLabelProps extends IHasCustomClasses {
  label: string
  showLabel?: boolean
}

interface IListboxOptionProps extends IHasCustomClasses {
  value: any
}

export const ListboxSelector = <TValue extends unknown>({
  label,
  showLabel = false,
  items,
  initialSelected,
  classes,
  labelClasses,
  buttonClasses,
  optionsClasses,
  optionClasses,
  onUpdate = () => { }
}: IListboxSelectorProps<TValue>) => {

  const [selected, setSelected] = useState<IListboxOption<TValue> | null>(initialSelected)

  useEffect(() => {
    selected && onUpdate(selected)
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [selected])

  useEffect(() => {
    !selected && setSelected(initialSelected)
  }, [initialSelected, selected])

  return (
    <Listbox value={selected} onChange={setSelected} by="value">
      <div className={combineClassNames("relative col-span-1", classes)}>
        <ListboxLabel label={label} showLabel={showLabel} classes={labelClasses} />
        <ListboxButton classes={buttonClasses}>
          {selected ? selected.label : label}
        </ListboxButton>
        <FadeInOut>
          <ListboxOptions classes={optionsClasses}>
            {items.length > 0 ? items.map((item, i) =>
              <ListboxOption key={i} value={item} classes={optionClasses}>
                {item.label}
              </ListboxOption>
            ) : (
              <NoItems />
            )}
          </ListboxOptions>
        </FadeInOut>
      </div>
    </Listbox>
  )
}

export const ListboxMultiSelector = <TValue extends unknown>({
  label,
  showLabel = false,
  items,
  initialSelected = [],
  classes,
  labelClasses,
  buttonClasses,
  optionsClasses,
  optionClasses,
  onUpdate = () => { }
}: IListboxMultiSelectorProps<TValue>) => {

  const [selected, setSelected] = useState<IListboxOption<TValue>[]>(initialSelected)

  useEffect(() => {
    onUpdate(selected)
  }, [selected, onUpdate])

  return (
    <Listbox value={selected} by="value" onChange={setSelected} multiple>
      <div className={combineClassNames("relative col-span-1", classes)}>
        <ListboxLabel label={label} showLabel={showLabel} classes={labelClasses} />
        <ListboxButton classes={buttonClasses}>
          {selected.length > 0
            ? selected?.map(s => s.label).join(', ')
            : label
          }
        </ListboxButton>
        <FadeInOut>
          <ListboxOptions classes={optionsClasses}>
            {items.length > 0 ? items.map((item, i) =>
              <ListboxOption key={i} value={item} classes={optionClasses}>
                {item.label}
              </ListboxOption>
            ) : (
              <NoItems />
            )}
          </ListboxOptions>
        </FadeInOut>
      </div>
    </Listbox>
  )
}

const ListboxLabel = ({ classes, label, showLabel = false }: IListboxLabelProps) => {
  return (
    <>
      {showLabel && <label className={combineClassNames("block font-bold text-sm text-gray-500 mb-1 uppercase", classes)}>{label}</label>}
    </>
  )
}

const ListboxButton = ({ classes, children }: IHasCustomClasses & IChildrenProps) => {
  return (
    <Listbox.Button className={combineClassNames(
      "h-10 w-full relative py-2 pl-3 pr-10 text-left bg-gray-900 rounded focus:outline-none focus-visible:outline-blue-500 focus-visible:outline-1",
      classes
    )}>
      <span className="block truncate tracking-wider">
        {children}
      </span>
      <span className="absolute inset-y-0 right-0 flex items-center pr-2">
        <SelectorIcon
          className="h-5 w-5 dark:group-hover:text-blue-500 group-hover:text-blue-900 transition-colors"
          aria-hidden="true"
        />
      </span>
    </Listbox.Button>
  )
}

const ListboxOptions = forwardRef(({ classes, children }: IHasCustomClasses & IChildrenProps, ref: LegacyRef<HTMLUListElement>) => {
  return (
    <Listbox.Options ref={ref} className={combineClassNames(
      "w-full focus:outline-none absolute origin-top-right right-0 z-50 mt-1 overflow-auto rounded-md py-1 text-base ring-1 ring-black ring-opacity-5",
      "bg-gray-200 dark:bg-slate-800",
      classes
    )}>
      {children}
    </Listbox.Options>
  )
})

const ListboxOption = ({ classes, value, children }: IListboxOptionProps & IChildrenProps) => {
  return (
    <Listbox.Option
      className={({ active }) => combineClassNames(
        `relative cursor-pointer select-none py-2 pl-10 pr-4 transition-all 
        ${active && 'text-blue-500'}`, classes
      )}
      value={value}
    >
      {({ selected }) => (
        <>
          <span className={`block truncate font-semibold`}>
            {children}
          </span>
          {selected && (
            <span className="absolute inset-y-0 left-0 flex items-center pl-3 text-blue-500">
              <CheckIcon className="h-5 w-5" aria-hidden="true" />
            </span>
          )}
        </>
      )}
    </Listbox.Option>
  )
}

const NoItems = ({ classes }: IHasCustomClasses) => {
  return (
    <div className={combineClassNames("relative select-none py-2 pl-10 pr-4 transition-all", classes)}>
      <span className={combineClassNames("block truncate tracking-wider")}>No items to display</span>
    </div>
  )
}