import { useCallback, useEffect, useState } from "react"
import { InputLabel, SpinnerIcon, TagButton } from "."
import { ITag } from "../../models"
import { combineClassNames } from "../../services"
import { v4 as uuid } from 'uuid'
import { FieldHookConfig, useField } from "formik"
import { HelperMessage } from "./HelperMessage"

interface ITagInputProps {
  name: string
  tags: ITag[]
  availableTags: ITag[]
  update: (tags: ITag[]) => void
  label: string
  disabled?: boolean
  errors: any
  touched: any
  loading?: boolean
  helperMessage?: string
}

const TagInput = ({
  name,
  tags,
  availableTags,
  update,
  label,
  errors,
  touched,
  disabled = false,
  loading = false,
  helperMessage
}: ITagInputProps & FieldHookConfig<string>) => {
  const [, meta, helpers] = useField(name)
  const [tagsValue, setTagsValue] = useState<string | null>(null)

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    touch()
    setTagsValue(e.target.value)
  }

  const handleTagInputKeyDown = useCallback((e: KeyboardEvent) => {
    if (e.key === 'Enter') {
      e.preventDefault()
      updateTags()
    }
  }, [update, tags, tagsValue, availableTags])

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
    touch()
    update([...tags, tag])
  }

  const removeTag = (tag: ITag) => {
    touch()
    update(tags.filter(t => t.id !== tag.id))
  }

  const touch = () => {
    if (!meta.touched) {
      helpers.setTouched(true)
    }
  }

  const filteredTags: ITag[] = availableTags
    .filter(at =>
    (at.value.toLowerCase().includes(tagsValue?.toLowerCase() ?? '') &&
      (!tags.map(t => t.id).includes(at.id))))
    .slice(0, 5)

  const focusInput = () => {
    document.getElementById("tag-input")?.focus();
  }

  useEffect(() => {
    document.getElementById("tag-input")?.addEventListener('keydown', handleTagInputKeyDown)

    return (() => {
      document.getElementById("tag-input")?.removeEventListener('keydown', handleTagInputKeyDown)
    })
  }, [handleTagInputKeyDown])

  return (
    <div className="flex flex-col">
      <InputLabel
        htmlFor="tag-input"
        label={label}
      >
        {touched && errors && (
          <span
            className={combineClassNames(
              "text-sm font-semibold text-rose-500 pointer-events-none"
            )}
          >
            {errors}
          </span>
        )}
        {helperMessage && (
          <HelperMessage message={helperMessage} />
        )}
      </InputLabel>
      <button id="tags-input" className="py-1 px-2.5" type="button" onClick={focusInput}>
        <div className="flex items-center gap-1 flex-wrap">
          {tags.map(tag => {
            return (
              <TagButton key={tag.id} tag={tag} onClick={() => removeTag(tag)} />
            )
          })}
          <input
            id="tag-input"
            className={combineClassNames(
              "placeholder-transparent h-8 px-1 flex-1 min-w-0",
              "bg-transparent",
              "focus-visible:outline-none",
              "autofill:shadow-fill-gray-700 autofill:text-fill-gray-100"
            )}
            type="string"
            placeholder={label}
            disabled={disabled}
            value={tagsValue ?? ''}
            onChange={(e) => handleChange(e)}
          />
        </div>
      </button>
      <div className="flex gap-1 items-center">
        <span className="inline-flex items-center text-sm">Suggested tags: </span>
        {loading ? (
          <SpinnerIcon className="w-4 h-4" />
        ) : (
          filteredTags.map((tag: ITag) => (
            <TagButton key={tag.id} tag={tag} onClick={() => addTag(tag)} tagSize="small" />
          ))
        )}
      </div>
    </div>
  )
}

export default TagInput