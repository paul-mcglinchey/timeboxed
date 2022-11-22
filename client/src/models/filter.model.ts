export interface IFilter {
  [key: string]: {
    value: any | null,
    label: string
  }
}