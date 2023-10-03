import { MagnifyingGlassIcon, XMarkIcon } from "@heroicons/react/24/solid";
import { Dispatch, SetStateAction, useCallback, useEffect, useState } from "react";
import { IFilter, IFilterableField } from "../../models";
import { combineClassNames } from "../../services";
import { ListboxSelector } from "./ListboxSelector";

interface ISearchBarProps {
  setFilter: Dispatch<SetStateAction<IFilter>>
  initialFilterField?: IFilterableField
  filterableFields?: IFilterableField[]
  backgroundColorClasses?: string
  id?: string
}

const SearchBar = ({ setFilter, initialFilterField, filterableFields = [], backgroundColorClasses = "bg-gray-300 dark:bg-gray-800", id = "search-bar" }: ISearchBarProps) => {

  const [filterValue, setFilterValue] = useState<string | null>(null)

  const clearFilter = () => {
    setFilterValue(null)
    setFilter(f => ({ ...f, value: null }))
  }

  const updateFilterField = (filterField: IFilterableField) => {
    setFilter(f => ({ ...f, ...filterField }))
  }

  const handleSearchBarOnBlur = () => {
    setFilter(f => ({ ...f, value: filterValue }))
  }
  
  const handleWindowKeyDown = useCallback((e: KeyboardEvent) => {
    if (e.ctrlKey && e.key === 'k') {
      e.preventDefault()
      document.getElementById(id)?.focus()
    }
  }, [])
  
  const handleSearchBarKeyDown = useCallback((e: KeyboardEvent) => {
    if (e.key === 'Enter') {
      setFilter(f => ({ ...f, value: filterValue }))
    }
  }, [filterValue, setFilter])

  useEffect(() => {
    if (filterableFields.length === 1) {
      updateFilterField(filterableFields[0])
    }
    
    window.addEventListener('keydown', handleWindowKeyDown)
    document.getElementById(id)?.addEventListener('keydown', handleSearchBarKeyDown)

    return () => {
      window.removeEventListener('keydown', handleWindowKeyDown)
      document.getElementById(id)?.removeEventListener('keydown', handleSearchBarKeyDown)
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
        <MagnifyingGlassIcon className="w-5 h-5" />
      </button>
      <div className="flex flex-1 relative">
        <input
          id={id}
          placeholder="Search entries..."
          value={filterValue ?? ''}
          onChange={(e) => setFilterValue(e.target.value)}
          onBlur={handleSearchBarOnBlur}
          className={combineClassNames(
            "flex flex-1 py-2 px-4 pl-0 sm:pl-20 focus:sm:pl-0 focus-visible:outline-none focus:outline-none caret-blue-500 dark:text-gray-400 text-gray-800 peer rounded-r-md sm:rounded-none border-0 focus:border-0 align-middle",
            !initialFilterField && filterableFields.length === 0 && "rounded-r-md",
            backgroundColorClasses
          )}
        />
        <div className="absolute hidden sm:block p-1 top-2 font-bold uppercase dark:text-white text-gray-500 text-xs rounded-md transform origin-left peer-focus:scale-x-0 transition-transform select-none">
          Ctrl + K
        </div>
        {filterValue && (
          <button onClick={clearFilter} className="absolute right-2 top-3 focus:outline outline-1 outline-offset-1 outline-blue-500 rounded text-gray-500">
            <XMarkIcon className="w-4 h-4" />
          </button>
        )}
      </div>
      {initialFilterField && filterableFields.length > 1 && (
        <ListboxSelector<string>
          initialSelected={{ value: initialFilterField.name, label: initialFilterField.label }}
          items={filterableFields.map(ff => ({ value: ff.name, label: ff.label }))}
          label="Searchable fields"
          classes="hidden sm:block"
          buttonClasses={combineClassNames(
            backgroundColorClasses,
            "hover:bg-gray-400 transition-colors p-4 rounded-none rounded-r-md"
          )}
          optionsClasses="w-auto"
          onUpdate={(item) => updateFilterField({ label: item.label, name: item.value })}
        />
      )}
    </div>
  )
}

export default SearchBar;