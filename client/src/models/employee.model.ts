export interface IEmployee {
  id: string
  firstName: string
  middleNames: string | string[]
  lastName: string
  firstLine: string
  secondLine: string
  thirdLine: string
  city: string
  country: string
  postCode: string
  primaryEmailAddress: string
  primaryPhoneNumber: string
  emails: string[]
  phoneNumbers: string[]
  birthDate: string | null
  startDate: string | null
  reportsTo: string | null
  role: string | null
  minHours: string
  maxHours: string
  unavailableDays: string[]
  holidays: {
    startDate: string
    endDate: string
    isPaid: boolean
  }[]
  colour: string
  deleted: boolean
}

export interface IAddEmployeeRequest {
  firstName: string | null
  lastName: string | null
  primaryEmailAddress: string | null
}

export interface IUpdateEmployeeRequest {
  firstName: string | null,
  middleNames: string | string[] | null,
  lastName: string | null
  firstLine: string | null 
  secondLine: string | null
  thirdLine: string | null
  city: string | null
  country: string | null
  postCode: string | null
  primaryEmailAddress: string | null
  primaryPhoneNumber: string | null
  birthDate: string | null,
  startDate: string | null,
  minHours: string | null
  maxHours: string | null
  unavailableDays: string[]
  colour: string | null
}

export interface IEmployeeResponse {
  employee: IEmployee
}

export interface IEmployeesResponse {
  count: number,
  items: IEmployee[]
}