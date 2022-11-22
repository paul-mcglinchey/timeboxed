export const combineClassNames = (...classes: Array<string | undefined | boolean>): string => {
  return classes.filter(Boolean).join(' ')
}