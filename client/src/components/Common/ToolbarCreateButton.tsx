import { combineClassNames } from '../../services';

interface IToolbarCreateButtonProps {
  content?: string
  action: () => void
}

const ToolbarCreateButton = ({ content, action }: IToolbarCreateButtonProps) => {

  return (
    <div className="hidden md:block relative mt-1">
      <button
        type="button"
        onClick={() => action()}
        className={combineClassNames(
          "h-10 w-full relative py-2 px-3 text-right font-semibold",
          "text-gray-200 bg-blue-500/70 dark:bg-gray-800 rounded-lg shadow-md focus:outline-none focus-visible:outline-blue-500 focus-visible:outline-1",
          "focus:outline-none focus-visible:outline-blue-500 focus-visible:outline-1",
          "hover:-translate-y-0.5 dark:hover:bg-blue-500 dark:hover:text-gray-900 transition-all"
        )}
      >
        <span className="block">{content || 'Create'}</span>
      </button>
    </div>
  )
}

export default ToolbarCreateButton;