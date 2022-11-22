export const getItemInLocalStorage = (itemName: string): string | undefined => {
  return localStorage.getItem(itemName) || undefined;
}

export const setItemInLocalStorage = (itemName: string, data: any) => {
  localStorage.setItem(itemName, data);
}

export const getJsonItemInLocalStorage = (itemName: string): string | undefined => {
  let data = localStorage.getItem(itemName);
  return JSON.parse(data || "");
}

export const setJsonItemInLocalStorage = (itemName: string, data: any) => {
  localStorage.setItem(itemName, JSON.stringify(data));
}

export const removeItemInLocalStorage = (itemName: string) => {
  localStorage.removeItem(itemName);
}