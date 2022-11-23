import { IFilter } from "../../models";

export interface IRequestBuilderService {
  buildRequest: (method?: string, bearerToken?: string | undefined, body?: any | undefined) => RequestInit
  buildQuery: (filters: IFilter) => string
}