import { useNotification } from "."

const useResolutionService = () => {
  const { notify } = useNotification()

  const handleResolution = (
    res: Response, 
    json: object | null, 
    verb?: string, 
    noun?: string, 
    successActions?: (() => void)[], 
    errorActions?: [() => void],
    notifyOnSuccess: boolean = true,
    notifyOnError: boolean = true
  ) => {
    ((res.ok && notifyOnSuccess) || (!res.ok && notifyOnError)) && notify(res, json, verb, noun)
    res.ok ? successActions?.forEach((action: () => void) => action()) : errorActions?.forEach((action: () => void) => action())
  }

  return { handleResolution }
}

export default useResolutionService