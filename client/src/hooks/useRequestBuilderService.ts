import { useCallback, useContext } from "react";
import { AuthContext } from "../contexts";
import { IFilter } from "../models";
import { IRequestBuilderService } from "./interfaces/requestbuilder.service.interface";

const useRequestBuilderService = (): IRequestBuilderService => {

  const token = useContext(AuthContext).getToken()

  const buildRequest = useCallback((method: string = "GET", bearerToken: string | undefined = undefined, body: any | undefined = undefined): RequestInit => {
    const request: RequestInit = {
      method: method,
      headers: {
        'Content-Type': 'application/json',
        'Authorization': (bearerToken || token) ? `Bearer ${bearerToken || token}` : ''
      }
    }
  
    if (body) {
      request.body = JSON.stringify(body);
    }
  
    return request;
  }, [token])

  const buildQuery = (filters: IFilter[]): string => {
    let query: string = ""

    for (let filter of filters) {
      if (filter && filter.value) query += `${query.includes('?') ? '&' : '?'}${filter.name}=${filter.value}`
    }

    return query
  }

  return { buildRequest, buildQuery }
}

export default useRequestBuilderService;