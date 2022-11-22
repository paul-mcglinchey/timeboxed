export const getItemInStorage = (itemName: string): string | undefined => {
  return sessionStorage.getItem(itemName) || undefined;
}

export const setItemInStorage = (itemName: string, data: any) => {
  sessionStorage.setItem(itemName, data);
}

export const getJsonItemInStorage = (itemName: string): string | undefined => {
  let data = sessionStorage.getItem(itemName);
  return JSON.parse(data || "");
}

export const setJsonItemInStorage = (itemName: string, data: any) => {
  sessionStorage.setItem(itemName, JSON.stringify(data));
}

export const removeItemInStorage = (itemName: string) => {
  sessionStorage.removeItem(itemName);
}