import { Transition } from "@headlessui/react"
import { XIcon } from "@heroicons/react/solid"
import { Form } from "formik"
import { Fragment, useEffect, useState } from "react"
import { IChildrenProps, IProps } from "../../models"
import { IApiError } from "../../models/error.model"

interface IFormikFormProps {
  error?: IApiError | undefined
}

export const FormikForm = ({ error, children, ...props }: IFormikFormProps & IChildrenProps & IProps) => {

  const [errorDismissed, setErrorDismissed] = useState<boolean>(false)
  const [prevError, setPrevError] = useState<IApiError>()
  const dismissError = () => {
    setPrevError(error)
    setErrorDismissed(true)
  }

  useEffect(() => {
    if (error?.id !== prevError?.id) setErrorDismissed(false)
  }, [error, prevError])

  return (
    <Form {...props}>
      <Transition
        as={Fragment}
        show={error !== undefined && !errorDismissed}
        enter="transition ease-in duration-100"
        enterFrom="opacity-0"
        enterTo="opacity-100"
        leave="transition ease-in duration-100"
        leaveFrom="opacity-100"
        leaveTo="opacity-0"
      >
        <div className="flex justify-between mt-4 ring-2 ring-red-500 rounded-md px-3 py-1 items-center space-x-2">
          <span>
            {error && error.message}
          </span>
          <button type="button" onClick={dismissError}>
            <XIcon className="w-5 h-5 text-red-500" />
          </button>
        </div>
      </Transition>
      {children}
    </Form>
  )
}