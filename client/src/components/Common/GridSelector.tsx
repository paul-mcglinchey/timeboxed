import { CSSProperties, ReactNode, Ref, useCallback, useEffect, useState } from "react"
import { combineClassNames } from "../../services"

interface IGridSelectorProps<T> {
  items: T[]
  columns?: 1 | 2 | 3 | 4 | 5 | 6 | 7
  initialSelected: T
  style?: (item: T) => CSSProperties
  classes?: string
  children?: ReactNode
  handleItemClick: (item: T) => void  
}

interface IGridPosition {
  x: number
  y: number
}

const GridSelector = <T,>({ 
  items, 
  columns = 5, 
  initialSelected,
  style, 
  classes, 
  children,
  myRef,
  handleItemClick
}: IGridSelectorProps<T> & { myRef: Ref<HTMLButtonElement>}) => {

  const [activePosition, setActivePosition] = useState<IGridPosition>({ x: -1, y: -1 })
  
  const xIndexMax = columns - 1
  const yIndexMax = Math.floor(items.length / columns)
  const lastRowIndexMax = (items.length % columns) - 1

  const getItemPosition = useCallback((index: number): { x: number, y: number } => {
    return {
      x: index % columns,
      y: Math.floor(index / columns)
    }
  }, [columns])

  const moveLeft = useCallback(() => setActivePosition(p => p.x <= 0 ? p : ({ ...p, x: p.x - 1 })), [])

  const moveRight = useCallback(() => setActivePosition(p => {
    if (p.y === yIndexMax && p.x >= lastRowIndexMax) return p
    if (p.x >= xIndexMax) return p

    return ({ ...p, x: p.x + 1 })
  }), [lastRowIndexMax, xIndexMax, yIndexMax])

  const moveUp = useCallback(() => setActivePosition(p => p.y <= 0 ? p : ({ ...p, y: p.y - 1 })), [])

  const moveDown = useCallback(() => setActivePosition(p => {
    if (p.y >= yIndexMax) return p
    if (p.y === (yIndexMax - 1) && lastRowIndexMax < p.x) return p

    return ({ ...p, y: p.y + 1 })
  }), [lastRowIndexMax, yIndexMax])

  const handleNavigation = useCallback((e: KeyboardEvent) => {
    if (e.key === 'ArrowLeft') moveLeft()
    if (e.key === 'ArrowRight') moveRight()
    if (e.key === 'ArrowUp') moveUp()
    if (e.key === 'ArrowDown') moveDown()
  }, [moveLeft, moveRight, moveUp, moveDown])

  useEffect(() => {
    setActivePosition(activePosition =>
      (!activePosition || (activePosition.x === -1 && activePosition.y === -1))
        ? getItemPosition(items.findIndex(i => i === initialSelected))
        : activePosition
    )
  }, [getItemPosition, initialSelected, items])

  useEffect(() => {
    document.getElementById("grid-selector")?.addEventListener('keydown', handleNavigation)

    let itemToFocusId: number = (activePosition.y * columns) + activePosition.x
    let itemToFocus = document.getElementById(`grid-selector-item-${itemToFocusId}`)

    itemToFocus?.focus()

    return () => {
      document.getElementById("grid-selector")?.removeEventListener('keydown', handleNavigation)
    }
  }, [activePosition, columns, handleNavigation])

  const getGridCols = (): string => {
    switch (columns) {
      case 1:
        return "grid-cols-1"
      case 2:
        return "grid-cols-2"
      case 3:
        return "grid-cols-3"
      case 4:
        return "grid-cols-4"
      case 5:
        return "grid-cols-5"
      case 6:
        return "grid-cols-6"
      case 7:
        return "grid-cols-7"
      default:
        return "grid-cols-1"
    }
  }

  return (
    <div id="grid-selector" className={combineClassNames(
      "grid gap-1", getGridCols()
    )}>
      {items.map((item, i) => (
        <button
          key={i}
          ref={myRef}
          tabIndex={-1}
          type="button"
          style={style && style(item)}
          id={`grid-selector-item-${i}`}
          className={combineClassNames(classes)}
          onClick={() => handleItemClick(item)}
        >{children && children}</button>
      ))}
    </div>
  )
}

export default GridSelector