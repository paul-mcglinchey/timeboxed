import { useCallback, useEffect, useState } from "react"
import { InputLabel, TagButton } from "."
import { ITag } from "../../models"
import { combineClassNames } from "../../services"
import { v4 as uuid } from 'uuid'

interface ITagInputProps {
  tags: ITag[]
  availableTags: ITag[]
  onChange: (tag: ITag[]) => void
  label: string
  disabled?: boolean
}

const TagInput = ({ tags, availableTags, onChange, label, disabled = false }: ITagInputProps) => {

  const [tagsValue, setTagsValue] = useState<string | null>(null)

  const handleTagInputKeyDown = useCallback((e: KeyboardEvent) => {
    if (e.key === 'Enter') {
      e.preventDefault()
      updateTags()
    }
  }, [onChange, tags, tagsValue, availableTags])

  const updateTags = () => {
    let value = tagsValue?.trim()

    if (value && value.length > 0) {
      // try and find the current valued tag in the existing tags
      let existingTag = availableTags.find(at => at.value === value)

      // if an existing tag was found matching the current value then use the existing value
      // otherwise generate a new tag
      if (existingTag) {
        addTag(existingTag)
      } else {
        addTag({ id: uuid(), value: value })
      }

      setTagsValue(null)
    } 
  }

  const addTag = (tag: ITag) => {
    onChange([...tags, tag])
  }

  const filteredTags: ITag[] = availableTags
    .filter(at => 
      (at.value.toLowerCase().includes(tagsValue?.toLowerCase() ?? '') &&
      (!tags.map(t => t.id).includes(at.id))))
    .slice(0, 5)

  useEffect(() => {
    document.getElementById("tag-input")?.addEventListener('keydown', handleTagInputKeyDown)

    return (() => {
      document.getElementById("tag-input")?.removeEventListener('keydown', handleTagInputKeyDown)
    })
  }, [handleTagInputKeyDown])

  return (
    <div className="relative mt-8 gap-y-2 flex flex-col">
      <div className={combineClassNames(
        "flex border-b dark:border-gray-300/20 border-gray-800/20"
      )}>
        <div className="flex items-center gap-1 flex-wrap">
          {tags.map(tag => {
            return (
              <TagButton key={tag.id} tag={tag} onClick={() => onChange(tags.filter(t => t.id !== tag.id))} />
            )
          })}
          <input
            id="tag-input"
            className={combineClassNames(
              "shrink placeholder-transparent h-10 px-1 peer min-w-0",
              "bg-transparent",
              "focus-visible:outline-none",
              "autofill:shadow-fill-gray-700 autofill:text-fill-gray-100"
            )}
            type="string"
            placeholder={label}
            disabled={disabled}
            value={tagsValue ?? ''}
            onChange={(e) => setTagsValue(e.target.value)}
          />
        </div>
        <InputLabel
          htmlFor="tag-input"
          label={label}
          visibilityConditions={[tags.length === 0]}
        />
      </div>
      <div className="flex gap-1 items-center">
        <span className="text-sm">Suggested tags: </span>
        {filteredTags.map((tag: ITag) => (
          <TagButton key={tag.id} tag={tag} onClick={() => onChange([...tags, tag])} tagSize="small" />
        ))}
      </div>
    </div>
  )
}

export default TagInput