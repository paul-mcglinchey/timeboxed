import { ITag } from "../../models"
import { combineClassNames } from "../../services"

interface ITagButtonProps {
  tag: ITag
  onClick: () => void
  tagSize?: 'small' | 'large'
}

const TagButton = ({ tag, onClick, tagSize = 'large' }: ITagButtonProps) => {
  return (
    <div className="h-8 flex items-center">
      <button
        type="button"
        onClick={onClick}
        className={combineClassNames(
          "px-1 rounded whitespace-nowrap",
          tagSize === 'small' ? "underline underline-offset-2 decoration-green-500 text-sm" : "bg-blue-800 text-green-300"
        )}>
        {tag.value}
      </button>
    </div>
  )
}

export default TagButton