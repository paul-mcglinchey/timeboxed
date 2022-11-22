import { Dispatch, SetStateAction } from "react";

export interface IPageable {
  pageNumber: number,
  setPageNumber: Dispatch<SetStateAction<number>>,
  pageSize: number,
  setPageSize: Dispatch<SetStateAction<number>>,
}