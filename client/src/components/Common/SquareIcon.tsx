import { combineClassNames } from "../../services"

interface ISquareIconProps {
  Icon: any
  size?: "sm" | "md" | "lg" | "xl"
  colour?: string
}

const SquareIcon = ({ Icon, size = "md", colour = "text-current" }: ISquareIconProps) => {
  return (
    <Icon className={combineClassNames(
      colour,
      size === "sm" && "w-4 h-4",
      size === "md" && "w-6 h-6",
      size === "lg" && "w=8 h-8",
      size === "xl" && "w-10 h-10"
    )}/>
  )
}

export default SquareIcon