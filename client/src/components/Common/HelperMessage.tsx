import { QuestionMarkCircleIcon } from "@heroicons/react/20/solid"

interface IHelperMessageProps {
  message: string
}

export const HelperMessage = ({ message }: IHelperMessageProps) => {
  return (
    <div className="relative p-1">
      <QuestionMarkCircleIcon className="w-6 h-6 text-blue-500/90 peer hover:opacity-80 transition-all" />
      <div className="absolute ring-2 ring-offset-1 ring-offset-transparent ring-blue-500 bg-blue-100 whitespace-nowrap dark:bg-blue-900 text-xs font-semibold p-2 rounded-md origin-top-right right-1 top-9 hidden peer-hover:block">
        {message}
      </div>
    </div>
  )
}
