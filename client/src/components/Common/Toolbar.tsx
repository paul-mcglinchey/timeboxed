import { IToolbarProps } from "../../models"

const Toolbar = ({ children, title }: IToolbarProps) => {

  return (
    <div className="flex-col xl:flex">
      <div className="flex sm:flex-row flex-col sm:space-y-0 space-y-4 justify-between pb-4">
        <div className="flex items-center space-x-2">
          <h1 className="text-3xl font-bold tracking-wider text-color-header">{title}</h1>
        </div>
        <div className="flex space-x-0 sm:space-x-4 items-center">
          {children}
        </div>
      </div>
    </div>
  )
}

export default Toolbar;