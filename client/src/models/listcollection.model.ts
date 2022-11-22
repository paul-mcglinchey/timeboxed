import { IAudit } from "."

export interface IListValue {
  _id?: string, 
  short?: string,
  long?: string,
  colour?: string
}

export interface IList {
  _id: string,
  name: string,
  description: string,
  values: IListValue[]
}

export interface IListCollection {
  _id?: string,
  lists: IList[],
  audit?: IAudit,
  system?: boolean,
  createdAt?: string,
  updatedAt?: string
}

export interface IListCollectionResponse {
  listcollection: IListCollection[]
}