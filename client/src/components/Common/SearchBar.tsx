import { SearchIcon } from "@heroicons/react/solid";
import { Dispatch, SetStateAction, useCallback, useEffect, useState } from "react";
import { IFilter, IFilterableField } from "../../models";
import { ListboxSelector } from "./ListboxSelector";
import SquareIconButton from "./SquareIconButton";

interface ISearchBarProps {
  setFilter: Dispatch<SetStateAction<IFilter>>
  initialFilterField: IFilterableField
  filterableFields: IFilterableField[]
}

const SearchBar = ({ setFilter, initialFilterField, filterableFields }: ISearchBarProps) => {

  const [filterValue, setFilterValue] = useState<string | null>(null)

  const updateFilterField = (filterField: IFilterableField) => {
    setFilter(f => ({ ...f, ...filterField }))
  }

  const handleKeyDown = useCallback((e: KeyboardEvent) => {
    if (e.key === 'Enter') {
      console.log(e.key + ' pressed')
      setFilter(f => ({ ...f, value: filterValue}))
    } 
  }, [filterValue, setFilter])

  useEffect(() => {
    document.getElementById("search-bar")?.addEventListener('keydown', handleKeyDown)

    return () => {
      document.getElementById("search-bar")?.removeEventListener('keydown', handleKeyDown)
    }
  }, [handleKeyDown])

  return (
    <div className="py-1 flex justify-between items-center text-gray-400 space-x-4 bg-gray-800 px-4 rounded-md shadowj">
      <div className="flex flex-grow space-x-4 items-center">
        <input id="search-bar" value={filterValue ?? ''} onChange={(e) => setFilterValue(e.target.value)} className="flex flex-1 px-2 my-2 bg-transparent outline-none focus:outline-none text-gray-400" />
        <ListboxSelector<string>
          initialSelected={{ value: initialFilterField.name, label: initialFilterField.label }}
          items={filterableFields.map(ff => ({ value: ff.name, label: ff.label }))}
          label="Searchable fields"
          buttonClasses="bg-transparent hover:bg-gray-700 transition-colors p-4"
          optionsClasses="w-40"
          onUpdate={(item) => updateFilterField({ label: item.label, name: item.value })}
        />
        <SquareIconButton action={() => setFilter(f => ({ ...f, value: filterValue }))} Icon={SearchIcon} className="w-6 h-6" />
      </div>
    </div>
  )
}

export default SearchBar;