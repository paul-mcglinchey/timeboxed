import { colours } from "../config";

export const generateColour = (): string => {
  return colours[Math.floor(Math.random() * colours.length)] || "#f43f5e" ;
}