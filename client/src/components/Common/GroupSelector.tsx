
import { ListboxSelector } from ".";
import { combineClassNames } from "../../services";
import { useContext } from "react";
import { GroupContext } from "../../contexts";
import { useNavigate } from "react-router-dom";

interface IGroupSelectorProps {
  fillContainer?: boolean
}

const GroupSelector = ({ fillContainer = false }: IGroupSelectorProps) => {
  const { groups = [], currentGroup, setCurrentGroupId } = useContext(GroupContext)
  const navigate = useNavigate()

  const handleUpdate = (id: string) => {
    if (currentGroup?.id !== id) {
      navigate('/dashboard')
    }

    setCurrentGroupId(id)
  }

  return (
    <div className="flex flex-grow items-center justify-end min-w-">
      <ListboxSelector<string>
        label="Groups"
        items={groups.map(g => ({ value: g.id, label: g.name }))} 
        initialSelected={currentGroup ? { value: currentGroup.id, label: currentGroup.name } : null}
        classes={combineClassNames(fillContainer && "w-full")}
        buttonClasses="bg-transparent shadow-none group"
        optionsClasses="min-w-max text-gray-800 dark:text-gray-200"
        onUpdate={(selected) => handleUpdate(selected.value)}
      />
    </div>
  )
}

export default GroupSelector