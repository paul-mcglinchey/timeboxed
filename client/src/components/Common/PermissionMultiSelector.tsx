import { useContext } from "react"
import { ListboxMultiSelector } from "."
import { MetaInfoContext } from "../../contexts/MetaInfoContext"
import { IListboxOption } from "../../models"

interface IPermissionMultiSelectorProps {
  label: string,
  showLabel?: boolean
  initialSelected: IListboxOption<number>[]
  onUpdate: (selected: IListboxOption<number>[]) => void
  classes?: string
  selectorClasses?: string
  optionsClasses?: string
}

const PermissionMultiSelector = (props: IPermissionMultiSelectorProps) => {

  const { permissions } = useContext(MetaInfoContext)

  return (
    <ListboxMultiSelector<number>
      {...props}
      items={permissions.map(p => ({ label: p.name, value: p.id }))}
    />
  )
}

export default PermissionMultiSelector