import { IChildrenProps } from "../../models";

const FormGrouping = ({ children }: IChildrenProps) => {
  return (
    <div className="flex flex-col gap-4 mb-6 mt-4">
      {children}
    </div>
  )
}

export default FormGrouping