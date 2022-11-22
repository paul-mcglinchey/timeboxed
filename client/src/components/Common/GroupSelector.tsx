
import { useGroupService } from "../../hooks";
import { ListboxSelector } from ".";

const GroupSelector = () => {
  const { groups = [], getGroup, currentGroup, setCurrentGroup } = useGroupService()

  return (
    <div className="flex flex-grow items-center justify-end min-w-">
      <ListboxSelector<string>
        label="Groups"
        items={groups.map(g => ({ value: g.id, label: g.name }))} 
        initialSelected={currentGroup ? { value: currentGroup.id, label: currentGroup.name } : null}
        buttonClasses="bg-transparent shadow-none group"
        optionsClasses="min-w-max text-gray-800 dark:text-gray-200"
        onUpdate={(selected) => setCurrentGroup(getGroup(selected.value))}
      />
    </div>
  )
}

export default GroupSelector