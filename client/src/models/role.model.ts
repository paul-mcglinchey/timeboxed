export interface IRole {
  id: string
  name: string
  description: string
  applicationId: number | null
  permissions: number[]
}

export interface IRolesResponse {
  count: number
  items: IRole[]
}