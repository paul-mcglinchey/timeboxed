import { ISession, ITracking } from "."

export interface IClient extends ITracking {
  id: string
  firstName: string | null,
  middleNames: string | string[] | null,
  lastName: string | null
  primaryEmailAddress: string | null
  primaryPhoneNumber: string | null
  emails: string[]
  phoneNumbers: string[]
  firstLine: string | null,
  secondLine: string | null,
  thirdLine: string | null,
  city: string | null,
  country: string | null,
  postCode: string | null,
  birthDate: string | null
  sessions: ISession[]
  colour: string
}

export interface IAddClientRequest {
  firstName: string | null
  lastName: string | null
  primaryEmailAddress: string | null
}

export interface IUpdateClientRequest {
  firstName: string | null
  middleNames: string | string[] | null,
  lastName: string | null
  primaryEmailAddress: string | null
  primaryPhoneNumber: string | null
  firstLine: string | null
  secondLine: string | null
  thirdLine: string | null
  city: string | null
  country: string | null
  postCode: string | null
  birthDate: string | null
  colour: string | null
}

export interface IClientsResponse {
  count: number
  items: IClient[]
}