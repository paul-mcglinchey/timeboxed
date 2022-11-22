export interface ITag {
  value: string,
  id: string
}

export interface ISession {
  id: string
  title: string,
  description: string | null,
  tags: ITag[],
  sessionDate: string | null
}

export interface IAddSessionRequest {
  title: string | null
  description: string | null
  tags: ITag[]
  sessionDate: string | null
}

export interface IUpdateSessionRequest {
  title: string | null
  description: string | null
  tags: ITag[]
  sessionDate: string | null
}