import { Fragment } from 'react';
import { Menu, Transition } from '@headlessui/react';
import { ChevronDownIcon } from '@heroicons/react/solid';
import { combineClassNames } from '../../services';

interface ISelectorProps {
  options: { value: any, label: any }[],
  option: any | undefined,
  setValue: (value: any) => void,
  label: string
}

const Selector = ({ options, option, setValue, label }: ISelectorProps) => {
  return (
    <div className="flex flex-col flex-1">
      <div className="text-gray-500 uppercase font-bold">{label}</div>
      <Menu as="div" className="h-10 rounded py-2 px-4 relative my-1 bg-gray-900 flex text-gray-300">
        <Menu.Button className="flex justify-between items-center font-semibold tracking-wide w-full text-left">
          <div className="flex-grow">
            {option && option.label ? option.label : label}
          </div>
          <ChevronDownIcon className="ml-1 w-6 h-6" />
        </Menu.Button>
        <Transition
          as={Fragment}
          enter="transition ease-out duration-100"
          enterFrom="transform opacity-0 scale-y-75"
          enterTo="transform opacity-100 scale-y-100"
          leave="transition ease-in duration-75"
          leaveFrom="transform opacity-100 scale-y-100"
          leaveTo="transform opacity-0 scale-y-75"
        >
          <Menu.Items className="z-50 origin-top-right absolute left-0 top-full mt-2 flex flex-col bg-gray-900 rounded shadow-md w-full">
            {options.length > 0 ? options.map((o, i) => (
              <Menu.Item key={i}>
                {({ active }) => (
                  <button
                    type="button"
                    className={combineClassNames(
                      "flex-grow py-2 px-4 text-left text-gray-400 hover:text-gray-200 tracking-wide rounded font-bold",
                      active ? "bg-blue-500" : "bg-gray-900"
                    )}
                    onClick={() => setValue(o)}
                  >
                    {o.label}
                  </button>
                )}
              </Menu.Item>
            )) : (
              <div className="flex p-2 text-left text-gray-400 tracking-wide rounded font-bold">
                No options found
              </div>
            )}
          </Menu.Items>
        </Transition>
      </Menu>
    </div>
  )
}

export default Selector;