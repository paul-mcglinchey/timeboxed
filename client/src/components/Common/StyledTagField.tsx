import { useEffect, useState } from "react"
import { v4 as uuidv4 } from 'uuid'
import { ITag } from "../../models"

interface ITagFieldProps {
  name: string,
  tags: ITag[],
  setTags: (tags: ITag[]) => void,
  label: string,
  errors: any,
  touched: any
}

const StyledTagField = ({ name, tags, setTags, label }: ITagFieldProps) => {

  const [tag, setTag] = useState<string>('');

  useEffect(() => {
    document.getElementById("tagField")!.addEventListener("keydown", handleKeyDown);
  });

  const handleKeyDown = (e: KeyboardEvent) => {
    if (tag.length === 0) {
      return;
    }

    if (e.code === 'Enter') {
      e.preventDefault();
      addTag();
    }
  }

  const addTag = () => {
    setTags([...tags, { value: tag, id: uuidv4() }])
    setTag('');
  }

  const removeTag = (tagId: string) => {
    setTags(tags.filter(t => t.id !== tagId));
  }

  return (
    <div className="flex flex-1 flex-col">
      <div className="flex justify-between">
        <label className="block font-bold text-gray-500 mb-1 uppercase">
          {label}
        </label>
      </div>
      <div className="flex w-full px-2 py-1 text-gray-200 items-center space-x-2 border-2 border-transparent bg-gray-800 focus-within:border-blue-500 rounded-sm">
        <div className="flex flex-1 flex-wrap">
          {tags && (
            tags.map((t) => (
              <button
                className="bg-gray-900 px-2 m-0.5 rounded hover:bg-red-500 transition-all select-none text-left"
                key={t.id}
                onClick={() => removeTag(t.id)}
              >
                {t.value}
              </button>
            ))
          )}
          <input className="flex-1 whitespace-pre-wrap bg-transparent focus:outline-none m-0.5" id="tagField" name={name} value={tag} onChange={(e) => setTag(e.target.value)} />
        </div>
      </div>
    </div>
  )
}

export default StyledTagField;