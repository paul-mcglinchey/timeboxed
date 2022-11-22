import { Field } from "formik"
import { TrashIcon } from "@heroicons/react/solid"
import { generateColour } from "../../services"
import { Button, ColourPicker } from "../Common"

interface IListItemProps {
  name: string
  setFieldValue: (field: string, value: string) => void
  remove: (index: number) => void
  index: number
  colour: string | undefined
}

interface IListItemFieldProps {
  name: string,
  placeholder: string
}

const ListItemField = ({ name, placeholder }: IListItemFieldProps) => {
  return (
    <Field
      name={name}
      placeholder={placeholder}
      className="bg-gray-900 rounded px-3 py-2 autofill:shadow-fill-gray-800"
    />
  )
}

const ListItem = ({ name, setFieldValue, remove, index, colour }: IListItemProps) => {
  return (
    <div className="flex items-center justify-between">
      <div className="flex space-x-4">
        <ListItemField name={`${name}.long`} placeholder="Descriptive item name" />
        <ListItemField name={`${name}.short`} placeholder="Item code" />
      </div>
      <div className="flex space-x-4">
        <ColourPicker
          hideIcon
          square
          colour={colour || generateColour()}
          setColour={(pc) => setFieldValue(`${name}.colour`, pc)}
        />
        <Button type="button" buttonType="Cancel" action={() => remove(index)} Icon={TrashIcon} />
      </div>
    </div>
  )
}

export default ListItem