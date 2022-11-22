import { stringInsert } from "./string.service";

export const numberParser = (num: number) => {
  var numAsString: string = num.toString();
  var sectors: number = numAsString.length <= 3 ? 0 : Math.floor(numAsString.length / 3);

  for (let i: number = 1; i <= sectors; i++) {
    numAsString = stringInsert((numAsString.length - (i * 3) - (i - 1)), ",");
  }

  return numAsString;
}