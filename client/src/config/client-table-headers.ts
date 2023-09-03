import { ITableHeaderItem } from "../models";

export const clientTableHeaders: ITableHeaderItem[] = [
  { name: "Name", value: "name", interactive: true },
  { name: "Last updated", value: "updatedAt", interactive: true },
  { name: "Sessions", value: "sessions", interactive: true },
  { name: "Options", value: "", interactive: false, hugRight: true },
]