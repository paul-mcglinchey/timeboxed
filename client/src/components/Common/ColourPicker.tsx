import { ChevronDownIcon } from "@heroicons/react/24/outline";
import { combineClassNames } from "../../services";
import { forwardRef, Ref } from "react";
import { colours } from "../../config";
import { FadeInOut, GridSelector } from ".";
import { Popover } from "@headlessui/react";

interface IColourPickerProps {
  menuSide?: 'LEFT' | 'RIGHT'
  colour: string,
  setColour: (colour: string) => void,
  hideIcon?: boolean,
  square?: boolean
}

const ColourPicker = forwardRef(({ menuSide = 'LEFT', colour, setColour, hideIcon, square }: IColourPickerProps, ref: Ref<HTMLButtonElement>) => {
  return (
    <Popover className="relative w-fit mb-1.5">
      <Popover.Button type="button" id="picker-button" className={combineClassNames("grid grid-cols-2 items-center")}>
        {!hideIcon && (
          <ChevronDownIcon className={combineClassNames("w-6 h-6 justify-self-center", menuSide === 'RIGHT' && "order-last")} />
        )}
        <div style={{ backgroundColor: colour }} className={`${square ? 'w-8' : 'w-full'} h-8 rounded`}></div>
      </Popover.Button>
      <FadeInOut>
        <Popover.Panel className={combineClassNames(
          menuSide === "RIGHT" ? "left-0 origin-top-left" : "right-0 origin-top-right",
          "z-50 mt-4 absolute w-max rounded bg-white dark:bg-gray-900 dark:ring-2 p-2 shadow-md"
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
      </FadeInOut>
    </Popover>
  )
})

export default ColourPicker;