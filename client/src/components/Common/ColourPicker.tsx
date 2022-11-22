import { ChevronDownIcon } from "@heroicons/react/outline";
import { combineClassNames } from "../../services";
import { forwardRef, Ref } from "react";
import { colours } from "../../config";
import { GridSelector } from ".";
import { Popover } from "@headlessui/react";

interface IColourPickerProps {
  menuSide?: 'LEFT' | 'RIGHT'
  colour: string,
  setColour: (colour: string) => void,
  hideIcon?: boolean,
  square?: boolean
}

const ColourPicker = forwardRef(({ menuSide = "RIGHT", colour, setColour, hideIcon, square }: IColourPickerProps, ref: Ref<HTMLButtonElement>) => {
  return (
    <div className="inline-flex items-center">
      <Popover className="relative w-full">
        <Popover.Button type="button" id="picker-button" className="flex w-full items-center space-x-2">
          {!hideIcon && (
            <ChevronDownIcon className="w-6 h-6" />
          )}
          <div style={{ backgroundColor: colour }} className={`${square ? 'w-8' : 'w-full'} h-8 rounded`}></div>
        </Popover.Button>
        <Popover.Panel className={combineClassNames(
          menuSide && menuSide === "LEFT" ? "left-0" : "-right-2",
          "z-50 origin-top-right mt-4 absolute w-max rounded bg-white dark:bg-gray-900 p-2 shadow-md"
        )}>
          {({ close }) => (
            <GridSelector<string>
              items={colours}
              initialSelected={colour}
              columns={7}
              style={(item) => ({ backgroundColor: item })}
              classes="w-8 h-8 rounded"
              myRef={ref}
              handleItemClick={(item) => {
                setColour(item)
                close()
              }}
            />
          )}
        </Popover.Panel>
      </Popover>
    </div>
  )
})

export default ColourPicker;