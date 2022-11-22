import { Notification } from "../enums";

export interface INotification {
  _id: string,
  message: string,
  type: Notification
};