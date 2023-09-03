import { IProps } from "../../models"
import SpinnerIcon from "./SpinnerIcon"

const SpinnerLoader = ({ className = "w-8 h-8" }: IProps) => {
  return (
    <div className="flex flex-1 justify-center"><SpinnerIcon className={className}/></div>
  )
}

export default SpinnerLoader