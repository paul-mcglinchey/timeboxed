import { Transition } from "@headlessui/react"
import { Fragment } from "react"
import { IChildrenProps } from "../../models"

interface ITransitionProps extends IChildrenProps {
  show?: boolean
}

export const FadeInOut = ({ children, show }: ITransitionProps) => {

  return (
    <Transition
      as={Fragment}
      show={show}
      enter="transition ease-in duration-100"
      enterFrom="opacity-0"
      enterTo="opacity-100"
      leave="transition ease-in duration-100"
      leaveFrom="opacity-100"
      leaveTo="opacity-0"
    >
      {children}
    </Transition>
  )
}