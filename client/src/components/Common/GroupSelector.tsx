
import { ListboxSelector } from ".";
import { combineClassNames } from "../../services";
import { useContext } from "react";
import { GroupContext } from "../../contexts";

interface IGroupSelectorProps {
  fillContainer?: boolean
}

const GroupSelector = ({ fillContainer = false }: IGroupSelectorProps) => {
  const { groups = [], getGroup, currentGroup, setCurrentGroup } = useContext(GroupContext)

  return (
    <div className="flex flex-grow items-center justify-end min-w-">
      <ListboxSelector<string>
        label="Groups"
        items={groups.map(g => ({ value: g.id, label: g.name }))} 
        initialSelected={currentGroup ? { value: currentGroup.id, label: currentGroup.name } : null}
        classes={combineClassNames(fillContainer && "w-full")}
        buttonClasses="bg-transparent shadow-none group"
        optionsClasses="min-w-max text-gray-800 dark:text-gray-200"
        onUpdate={(selected) => setCurrentGroup(getGroup(selected.value))}
      />
    </div>
  )
}

export default GroupSelector