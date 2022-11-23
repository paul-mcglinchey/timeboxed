import { SearchIcon } from "@heroicons/react/solid";
import { Dispatch, SetStateAction } from "react";
import { IFilter, IFilterableField } from "../../models";
import { ListboxSelector } from "./ListboxSelector";

interface ISearchBarProps {
  filter: IFilter,
  setFilter: Dispatch<SetStateAction<IFilter>>
  initialFilterField: IFilterableField
  filterableFields: IFilterableField[]
}

const SearchBar = ({ filter, setFilter, initialFilterField, filterableFields }: ISearchBarProps) => {

  const handleFilterValueUpdate = (value: string) => {
    setFilter(f => ({ ...f, value: value }));
  }

  const updateFilterField = (filterField: IFilterableField) => {
    setFilter(f => ({ ...f, ...filterField }))
  }

  return (
    <div className="py-1 flex justify-between items-center text-gray-400 space-x-4 bg-gray-800 px-4 rounded-md shadowj">
      <div className="flex flex-grow space-x-4 items-center">
        <input value={filter.value ?? ""} onChange={(e) => handleFilterValueUpdate(e.target.value)} className="flex flex-1 px-2 my-2 bg-transparent outline-none focus:outline-none text-gray-400" />
        <ListboxSelector<string>
          initialSelected={{ value: initialFilterField.name, label: initialFilterField.label }}
          items={filterableFields.map(ff => ({ value: ff.name, label: ff.label }))}
          label="Searchable fields"
          optionsClasses="w-40"
          onUpdate={(item) => updateFilterField({ label: item.label, name: item.value })}
        />
        <SearchIcon className="w-6 h-6" />
      </div>
    </div>
  )
}

export default SearchBar;