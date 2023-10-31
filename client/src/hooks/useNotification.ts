import { useContext } from "react"
import { Notification } from "../enums"
import { NotificationContext } from "../contexts"

const useNotification = () => {

  const notificationContext = useContext(NotificationContext)

  const notify = (res: Response, json: any, verb?: string, noun?: string) => {
    if (res.ok) {
      const message = `${verb && noun ? `Successfully ${verb}d ${noun}`: 'Success'}`
      notificationContext.addNotification(message, Notification.Success)
    } else {
      const message = `${res.status < 500 ? (json.message ?? res.statusText) : `A problem occurred${verb && noun ? ` ${verb.slice(0, verb.length - 1)}ing ${noun}` : '...'}`}`
      notificationContext.addNotification(message, Notification.Error)
    }
  }

  return { ...notificationContext, notify };
}

export default useNotification;