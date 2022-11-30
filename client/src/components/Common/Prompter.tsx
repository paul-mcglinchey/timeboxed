import { Transition } from '@headlessui/react';
import { useDelayedRendering } from '../../hooks';
import { Button } from '.';
import { IProps } from '../../models';

interface IPrompterProps {
  title: string,
  subtitle?: string,
  action?: () => void,
  Icon?: React.FC<IProps>
}

const Prompter = ({ Icon, title, subtitle, action }: IPrompterProps) => {

  const show = useDelayedRendering(100);

  return (
    <Transition
      show={show}
      enter="transition ease-out duration-100"
      enterFrom="transform opacity-0 scale-95"
      enterTo="transform opacity-100 scale-100"
      leave="transition ease-in duration-75"
      leaveFrom="transform opacity-100 scale-100"
      leaveTo="transform opacity-0 scale-95"
      className="flex mt-24 justify-center rounded-md"
    >
      <div className="flex flex-col p-4 rounded-lg">
        <div>
          <h2 className='whitespace-nowrap font-extrabold tracking-wide text-2xl mb-2'>
            {title}
          </h2>
          {subtitle && (
            <p className='tracking-wide'>
              {subtitle}
            </p>
          )}
        </div>
        {action && (
          <div className="flex justify-center">
            <Button action={() => action()} Icon={Icon} content='Lets go!' buttonType="Tertiary" XL />
          </div>
        )}
      </div>
    </Transition>
  )
}

export default Prompter;