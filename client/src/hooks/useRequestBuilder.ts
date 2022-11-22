import { useContext } from "react";
import { AuthContext } from "../contexts";
import { IFilter } from "../models";

const useRequestBuilder = () => {

  const token = useContext(AuthContext).getToken()

  const requestBuilder = (method: string = "GET", bearerToken: string | undefined = undefined, body: any | undefined = undefined): RequestInit  => {

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
  }

  const buildQuery = (filters: IFilter): string => {
    let query: string = ""

    Object.keys(filters).forEach((k, i) => {
      query += `${i === 0 ? '?' : '&'}${k}=${filters[k]?.value}`
    })

    return query
  }

  return { requestBuilder, buildQuery }
}

export default useRequestBuilder;