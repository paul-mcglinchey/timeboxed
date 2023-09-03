import { Fragment, ReactNode, useState } from "react";
import { Transition, Dialog } from "@headlessui/react";
import { combineClassNames } from "../../services";
import { DialogButton, Checkbox } from ".";

interface IModalProps {
  children?: (
    SubmissionButton: (
      content?: string | undefined, 
      action?: () => Promise<void> | void,
      submissionGate?: boolean,
      isLoading?: boolean
    ) => ReactNode
  ) => JSX.Element
  title: string,
  description: string,
  isOpen: boolean,
  close: () => void
  allowAddMultiple?: boolean
  level?: 1 | 2 | 3
  wide?: boolean
}

const Modal = ({ children, title, description, isOpen, close, level = 1, allowAddMultiple = false, wide = false }: IModalProps) => {

  const [keepOpen, setKeepOpen] = useState<boolean>(false)

  const handlePositiveAction = async (action?: () => Promise<void> | void) => {
    if (action) await action()

    if (!keepOpen) {
      close()
    }
  }

  return (
    <Transition appear show={isOpen} as={Fragment}>
      <Dialog as="div" className="relative z-10 text-color-paragraph" onClose={close}>
        <Transition.Child>
          <div className="fixed inset-0 bg-black bg-opacity-80" />
        </Transition.Child>
        <div className="fixed z-10 inset-0 overflow-y-auto">
          <div className="flex min-h-full md:items-start justify-center p-0 md:p-4 text-center">
            <Transition.Child
              as={Fragment}
              enter="ease-out duration-200"
              enterFrom="opacity-0 translate-y-1/4"
              enterTo="opacity-100 translate-y-0"
              leave="ease-in duration-200"
              leaveFrom="opacity-100 translate-y-0"
              leaveTo="opacity-0 translate-y-1/4"
            >
              <Dialog.Panel className={
                combineClassNames(
                  "w-full rounded-t-2xl md:rounded-2xl bg-slate-200 dark:bg-gray-800 p-2 py-6 md:p-6 text-left align-middle transition-all",
                  level === 1 && "mt-16", level === 2 && "mt-32", level === 3 && "mt-40",
                  level === 1 && (wide ? "max-w-7xl" : "max-w-4xl"),
                  level === 2 && (wide ? "max-w-4xl" : "max-w-2xl"),
                  level === 3 && (wide ? "max-w-2xl" : "max-w-xl")
                )}
              >
                <div className="flex justify-between mb-2 items-center">
                  <Dialog.Title as="h2" className="text-2xl font-semibold leading-6 text-color-header">
                    {title}
                  </Dialog.Title>
                  <DialogButton onClick={close}>
                    Cancel
                  </DialogButton>
                </div>
                <Dialog.Description className="sr-only">
                  {description}
                </Dialog.Description>

                {children && children((content, action, submissionGate = true, isLoading) =>
                  <div className={`flex mt-4 ${allowAddMultiple ? 'justify-between' : 'justify-end'}`}>
                    {allowAddMultiple && <Checkbox id="addMultiple" label="Add multiple" onChange={() => setKeepOpen(!keepOpen)} checked={keepOpen} />}
                    <DialogButton disabled={!submissionGate} type="submit" onClick={() => handlePositiveAction(action)} loading={isLoading}>{content || 'Submit'}</DialogButton>
                  </div>
                )}
              </Dialog.Panel>
            </Transition.Child>
          </div>
        </div>
      </Dialog>
    </Transition>
  )
}

export default Modal;