import { useFormikContext } from "formik";
import { useCallback, useEffect } from "react";
import { IProps } from "../../models";

const CustomDate = ({ field, ...props }: IProps) => {

  const { setFieldValue } = useFormikContext()
  
  const handleDateInputKeyDown = useCallback((e: KeyboardEvent) => {
    if (e.key === 'T' || e.key === 't') {
      setFieldValue(field, new Date())
    }
  }, [])

  console.log(field)
  useEffect(() => {
    document.getElementById(field.id)?.addEventListener('keydown', handleDateInputKeyDown)

    return () => {
      document.getElementById(field.id)?.removeEventListener('keydown', handleDateInputKeyDown)
    }
  }, [])

  return (
    <div>
      <input type="date" {...field} {...props} min="1900-01-01" placeholder="1970-01-01" />
    </div>
  )
}

export default CustomDate;