import { SearchIcon, XIcon } from "@heroicons/react/solid";
import { Dispatch, SetStateAction, useCallback, useEffect, useState } from "react";
import { IFilter, IFilterableField } from "../../models";
import { combineClassNames } from "../../services";
import { ListboxSelector } from "./ListboxSelector";

interface ISearchBarProps {
  setFilter: Dispatch<SetStateAction<IFilter>>
  initialFilterField?: IFilterableField
  filterableFields?: IFilterableField[]
  backgroundColorClasses?: string
}

const SearchBar = ({ setFilter, initialFilterField, filterableFields = [], backgroundColorClasses = "bg-gray-300 dark:bg-gray-800" }: ISearchBarProps) => {

  const [filterValue, setFilterValue] = useState<string | null>(null)

  const updateFilterField = (filterField: IFilterableField) => {
    setFilter(f => ({ ...f, ...filterField }))
  }

  const handleWindowKeyDown = useCallback((e: KeyboardEvent) => {
    if (e.ctrlKey && e.key === 'k') {
      e.preventDefault()
      document.getElementById("search-bar")?.focus()
    }
  }, [])
  
  const handleSearchBarKeyDown = useCallback((e: KeyboardEvent) => {
    if (e.key === 'Enter') {
      setFilter(f => ({ ...f, value: filterValue }))
    }
  }, [filterValue, setFilter])

  useEffect(() => {
    window.addEventListener('keydown', handleWindowKeyDown)
    document.getElementById("search-bar")?.addEventListener('keydown', handleSearchBarKeyDown)

    return () => {
      window.removeEventListener('keydown', handleWindowKeyDown)
      document.getElementById("search-bar")?.removeEventListener('keydown', handleSearchBarKeyDown)
    }
  }, [handleWindowKeyDown, handleSearchBarKeyDown])

  return (
    <div className="flex justify-between items-center rounded-md shadow">
      <button 
        className={combineClassNames(
          "h-10 py-2 px-4 rounded-l-md transition-colors hover:text-blue-500 focus-within:outline-none focus-within:text-blue-500",
          backgroundColorClasses
        )}
        onClick={() => setFilter(f => ({ ...f, value: filterValue }))}
      >
        <SearchIcon className="w-5 h-5" />
      </button>
      <div className="flex flex-1 relative">
        <input
          id="search-bar"
          placeholder="Search entries..."
          value={filterValue ?? ''}
          onChange={(e) => setFilterValue(e.target.value)}
          className={combineClassNames(
            "flex flex-1 py-2 px-4 sm:pl-20 focus:pl-0 focus-visible:outline-none caret-blue-500 text-gray-400 peer rounded-r-md sm:rounded-none",
            !initialFilterField && filterableFields.length === 0 && "rounded-r-md",
            backgroundColorClasses
          )}
        />
        <div className="absolute hidden sm:block p-1 top-2 font-bold uppercase text-white text-xs rounded-md transform origin-left peer-focus:scale-x-0 transition-transform select-none">
          Ctrl + K
        </div>
        {filterValue && (
          <button onClick={() => setFilterValue(null)} className="absolute right-2 top-3 focus:outline outline-1 outline-offset-1 outline-blue-500 rounded text-gray-500">
            <XIcon className="w-4 h-4" />
          </button>
        )}
      </div>
      {initialFilterField && filterableFields.length > 0 && (
        <ListboxSelector<string>
          initialSelected={{ value: initialFilterField.name, label: initialFilterField.label }}
          items={filterableFields.map(ff => ({ value: ff.name, label: ff.label }))}
          label="Searchable fields"
          classes="hidden sm:block"
          buttonClasses="bg-gray-800 hover:bg-gray-700 transition-colors p-4 rounded-none rounded-r-md"
          optionsClasses="w-40"
          onUpdate={(item) => updateFilterField({ label: item.label, name: item.value })}
        />
      )}
    </div>
  )
}

export default SearchBar;