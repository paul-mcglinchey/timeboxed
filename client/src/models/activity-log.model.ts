import { Task } from '../enums'

export interface IActivityLog {
  task: Task
  actor: string
  updatedAt: string
  createdAt: string
  _id?: string
}