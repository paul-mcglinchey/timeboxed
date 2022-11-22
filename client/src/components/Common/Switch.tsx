import { Switch as HeadlessSwitch } from '@headlessui/react'
import { combineClassNames } from '../../services'

interface ISwitchProps {
  enabled: boolean
  setEnabled: () => void
  description: string
  IconEnabled?: any
  IconDisabled?: any
}

const Switch = ({ enabled, setEnabled, description, IconEnabled, IconDisabled }: ISwitchProps) => {
  return (
    <HeadlessSwitch
      as='div'
      checked={enabled}
      onChange={setEnabled}
      className={`${
        enabled ? 'bg-green-500' : 'bg-gray-200 dark:bg-gray-900'
      } relative inline-flex h-6 w-11 items-center rounded-full transition-all`}
    >
      <span className="sr-only">{description}</span>
      <div
        className={combineClassNames(
          enabled ? 'translate-x-6' : 'translate-x-1',
          enabled ? !IconEnabled && 'bg-gray-900' : !IconDisabled && 'bg-green-500',
          'inline-block h-4 w-4 transform rounded-full text-gray-600 dark:text-gray-900 transition-all'
        )}
      >
        {enabled 
          ? IconEnabled && <IconEnabled className="w-4 h-4" />
          : IconDisabled && <IconDisabled className="w-4 h-4" />}
      </div>
    </HeadlessSwitch>
  )
}

export default Switch