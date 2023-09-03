export const getInitials = (phrase: string | undefined) => {
  if (!phrase) return "N/A"

  let initials = "";
  phrase.split(" ").forEach((w: string) => {
    initials += w[0];
  })

  return initials;
}

export const stringInsert = function (this: any, index: number, string: string): string {
  if (index > 0) {
    return this.substring(0, index) + string + this.substring(index);
  }

  return string + this;
}