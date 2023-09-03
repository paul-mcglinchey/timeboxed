import { Menu, Transition } from "@headlessui/react";
import { ChevronDownIcon } from "@heroicons/react/24/solid";
import { Fragment } from "react";
import { combineClassNames } from "../../services";

interface IOption {
  label: string,
  action?: () => void,
  Icon?: any,
  iconColour?: string
}

interface IDropdownProps {
  label?: string
  options?: IOption[]
}

const Dropdown = ({ label = "Options", options = [] }: IDropdownProps) => {
  return (
    <Menu as="div" className="relative inline-block text-left dark:text-white text-gray-800 w-full">
      <Menu.Button className={combineClassNames(
        "flex justify-center items-center w-full rounded-md",
        "dark:bg-gray-800 bg-gray-200 hover:text-blue-400 transition-colours text-base font-semibold focus:outline-none focus:text-blue-500",
        "px-1 py-0.5", "md:px-4"
      )}>
        <div className="hidden md:block">
          {label}
        </div>
        <div className="p-1 md:ml-2 md:pr-0">
          <ChevronDownIcon className="h-6 w-6" aria-hidden="true" />
        </div>
      </Menu.Button>

      <Transition
        as={Fragment}
        enter="transition ease-out duration-100"
        enterFrom="transform opacity-0 scale-95"
        enterTo="transform opacity-100 scale-100"
        leave="transition ease-in duration-75"
        leaveFrom="transform opacity-100 scale-100"
        leaveTo="transform opacity-0 scale-95"
      >
        <Menu.Items className="z-50 flex flex-col origin-top-right absolute right-0 mt-2 rounded dark:bg-blue-900 bg-blue-100 focus:outline-none shadow-md">
          {options.map((o: IOption, key: number) => (
            <Menu.Item key={key}>
              {({ active }) => (
                <button
                  onClick={() => o.action ? o.action() : {}}
                  className={combineClassNames(
                    active ? 'text-blue-400' : '',
                    'px-4 py-2 text-base font-semibold flex justify-between items-center space-x-8 whitespace-nowrap'
                  )}
                >
                  <span>
                    {o.label}
                  </span>
                  <span>
                    {o.Icon && (<o.Icon className={`w-4 h-4 ${o.iconColour}`} />)}
                  </span>
                </button>
              )}
            </Menu.Item>
          ))}
        </Menu.Items>
      </Transition>
    </Menu >
  )
}

export default Dropdown;