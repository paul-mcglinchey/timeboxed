export const parseTimeDifference = (target: string) => {
  const currentDate: Date = new Date();
  const milliDiff: number = currentDate.getTime() - new Date(target).getTime();
  const secondsDiff = milliDiff / 1000;
  const minutesDiff = secondsDiff / 60;
  const hoursDiff = minutesDiff / 60;
  const daysDiff = hoursDiff / 60;

  if (daysDiff >= 1) return `${new Date(target).toLocaleDateString()}`
  else if (hoursDiff >= 1) return `${Math.floor(hoursDiff)}h`
  else if (minutesDiff >= 1) return `${Math.floor(minutesDiff)}m`
  else if (secondsDiff >= 30) return `${Math.floor(secondsDiff)}s`
  else return "Just now";
}