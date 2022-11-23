import { useCallback, useEffect, useState } from "react"
import { InputLabel } from "."
import { combineClassNames } from "../../services"

interface ITagInputProps {
  tags: string[]
  onChange: (tag: string[]) => void
  label: string
  disabled?: boolean
}

const TagInput = ({ tags, onChange, label, disabled = false }: ITagInputProps) => {

  const [tagsValue, setTagsValue] = useState<string | null>(null)

  const handleTagInputKeyDown = useCallback((e: KeyboardEvent) => {
    if (e.key === 'Enter' || e.key === ' ') {
      e.preventDefault()
      if (tagsValue && tagsValue.trim().length > 0) {
        onChange([...tags, tagsValue.trim()])
        setTagsValue(null)
      } 
    }
  }, [onChange, tags, tagsValue])

  useEffect(() => {
    document.getElementById("tag-input")?.addEventListener('keydown', handleTagInputKeyDown)

    return (() => {
      document.getElementById("tag-input")?.removeEventListener('keydown', handleTagInputKeyDown)
    })
  }, [handleTagInputKeyDown])

  return (
    <div className="relative mt-8">
      <div className={combineClassNames(
        "flex border-b-2 border-gray-300/20"
      )}>
        <div className="flex items-center">
          {tags.map((tag: string, i: number) => {
            return (
              <button type="button" onClick={() => onChange(tags.filter(t => t !== tag))} key={i} className="inline-block mx-1 bg-blue-700 px-1 rounded">{tag}</button>
            )
          })}
        </div>
        <input
          id="tag-input"
          className={combineClassNames(
            "flex flex-1 placeholder-transparent h-10 px-1 peer",
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
        <InputLabel
          htmlFor="tag-input"
          label={label}
          visibilityConditions={[tags.length === 0]}
        />
      </div>
    </div>
  )
}

export default TagInput