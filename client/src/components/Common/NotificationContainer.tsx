import { memo } from "react";
import { useNotification } from "../../hooks";
import { INotification } from "../../models";
import { NotificationCard } from ".";

const NotificationContainer = () => {

  const { getNotifications, removeNotification } = useNotification();

  const notifications = getNotifications() || []

  return (
    <div className="fixed z-50 w-screen sm:w-auto top-0 right-0 space-y-4 overflow-hidden">
        {notifications.map((n: INotification, index: number) => (
          <NotificationCard notification={n} removeNotification={removeNotification} key={index} />
        ))}
    </div>
  )
}

export default memo(NotificationContainer);