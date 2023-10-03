export interface ITableHeader {
  headers: ITableHeaderItem[]
}

export interface ITable  {
  isLoading: boolean
  compact?: boolean
}

export interface ITableHeaderItem {
  name: string,
  value?: string,
  interactive?: boolean,
  hugRight?: boolean
}