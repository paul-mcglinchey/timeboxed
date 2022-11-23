import { SearchIcon, XIcon } from "@heroicons/react/solid";
import { Dispatch, SetStateAction, useCallback, useEffect, useState } from "react";
import { IFilter, IFilterableField } from "../../models";
import { ListboxSelector } from "./ListboxSelector";

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
      setFilter(f => ({ ...f, value: filterValue }))
    }
  }, [filterValue, setFilter])

  useEffect(() => {
    document.getElementById("search-bar")?.addEventListener('keydown', handleKeyDown)

    return () => {
      document.getElementById("search-bar")?.removeEventListener('keydown', handleKeyDown)
    }
  }, [handleKeyDown])

  return (
    <div className="flex justify-between items-center rounded-md shadow">
      <div className="flex flex-1 relative">
        <input id="search-bar" value={filterValue ?? ''} onChange={(e) => setFilterValue(e.target.value)} className="flex flex-1 py-2 px-4 rounded-l-md bg-gray-800 outline-none focus:outline-none text-gray-400" />
        {filterValue && (
          <button onClick={() => setFilterValue(null)} className="absolute right-2 top-3 focus-within:outline outline-1 outline-offset-1 outline-blue-500 rounded text-gray-500">
            <XIcon className="w-4 h-4" />
          </button>
        )}
      </div>
      <ListboxSelector<string>
        initialSelected={{ value: initialFilterField.name, label: initialFilterField.label }}
        items={filterableFields.map(ff => ({ value: ff.name, label: ff.label }))}
        label="Searchable fields"
        buttonClasses="bg-gray-800 hover:bg-gray-700 transition-colors p-4 rounded-none"
        optionsClasses="w-40"
        onUpdate={(item) => updateFilterField({ label: item.label, name: item.value })}
      />
      <button className="py-2 px-4 bg-gray-800 rounded-r-md transition-colors hover:text-blue-500 focus-within:outline-none focus-within:text-blue-500" onClick={() => setFilter(f => ({ ...f, value: filterValue }))}>
        <SearchIcon className="w-6 h-6" />
      </button>
    </div>
  )
}

export default SearchBar;