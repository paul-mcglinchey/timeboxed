import { WideIcon } from '.'
import { IChildrenProps } from '../../models'

const PublicWrapper = ({ children }: IChildrenProps) => {
  return (
    <div className="relative h-screen lg:h-auto lg:rounded-lg max-w-md lg:mt-12 mx-auto p-8">
      <div className="absolute top-24 right-16 text-xs font-black select-none pointer-events-none uppercase text-emerald-500 tracking-wide">Alpha</div>
      <WideIcon className="w-full sm:h-16 text-gray-300 mb-16" />
      <div className="rounded filter drop-shadow-sm">
        {children}
      </div>
    </div>
  )
}

export default PublicWrapper