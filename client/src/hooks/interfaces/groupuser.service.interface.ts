import { IGroupUserRequest } from "../../models"

export interface IGroupUserService {
  updateGroupUser: (groupId: string | undefined, userId: string | undefined, values: IGroupUserRequest) => void
  joinGroup: (groupId: string | undefined) => void
  isLoading: boolean
}