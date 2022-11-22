import { parse, isDate } from "date-fns";

export const parseDateString = (originalValue: string)  => {
  if (isDate(originalValue)) return originalValue;

  if (typeof originalValue === "string") {
    return parse(originalValue, "yyyy-MM-dd", new Date());
  } else {
    return parse(new Date().toString(), "yyyy-MM-dd", new Date());
  }
}

export const makeDate = (isoDate: Date, delimiter: string = '-') => {
  var date = new Date(isoDate);
  return `${date.getUTCDate()}${delimiter}${date.getUTCMonth() + 1}${delimiter}${date.getFullYear()}`;
}

export const makeUSDate = (isoDate: Date, delimiter: string = '-') => {
  var date = new Date(isoDate);
  return `${date.getFullYear()}${delimiter}${date.getUTCMonth() + 1}${delimiter}${date.getUTCDate()}`
}

export const compareDates = (dateOne: Date, dateTwo: Date) => {
  return {
    sameDay: (): boolean => dateOne.getUTCDate() === dateTwo.getUTCDate() && dateOne.getUTCFullYear() === dateTwo.getUTCFullYear() && dateOne.getUTCMonth() === dateTwo.getUTCMonth()
  }
}

export const getDateOnly = (date: Date): string => {
  date = new Date(date)
  return date.toISOString().split("T")[0]!
}