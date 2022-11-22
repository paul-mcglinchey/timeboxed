export interface ITableHeader {
  headers: Array<{
    name: string, value?: string, interactive?: boolean
  }>
}

export interface ITable  {
  isLoading: boolean
}