import { memo, useCallback, useEffect, useState } from 'react';
import { Transition } from '@headlessui/react';
import { XMarkIcon } from '@heroicons/react/24/outline';
import { combineClassNames } from '../../services';
import { INotification } from '../../models';
import { useDelayedRendering, useIsMounted } from '../../hooks';
import { Notification } from '../../enums';

interface INotificationCardProps {
  notification: INotification,
  removeNotification: (notificationId: string) => void
}

const NotificationCard = ({ notification, removeNotification }: INotificationCardProps) => {

  const show = useDelayedRendering(100);
  const [open, setOpen] = useState(true);
  const isMounted = useIsMounted();

  const close = useCallback(() => {
    isMounted() && setOpen(false);
    setTimeout(() => removeNotification(notification._id), 1000);
  }, [isMounted, removeNotification, notification._id])

  useEffect(() => {
    setTimeout(() => close(), 3000)
  }, [close])

  return (
    <Transition
      show={show && open}
      enter="transition ease-in-out duration-300"
      enterFrom="transform translate-x-96"
      enterTo="transform translate-x-0"
      leave="transition ease-in-out duration-300"
      leaveFrom="transform translate-x-0"
      leaveTo="transform translate-x-full"
      className={combineClassNames(
        "relative sm:w-96 box-border p-4 m-2 bg-gray-100 dark:bg-gray-900 border rounded-md drop-shadow-md flex flex-col",
        notification.type === Notification.Success ? "text-green-500 border-green-500" : notification.type === Notification.Error ? "text-red-500 border-red-500" : "text-gray-200 border-gray-200"
      )}
    >
      <div className="flex justify-between space-x-2 items-center">
        <span className="font-bold">{notification.message}</span>
        <button onClick={() => close()} className="flex items-center">
          <XMarkIcon className="w-5 h-5 text-gray-500" />
        </button>
      </div>
    </Transition>
  )
}

export default memo(NotificationCard);