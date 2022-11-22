import { IProps } from "../../models";

const CustomDate = ({ field, ...props }: IProps) => (
  <div>
    <input type="date" {...field} {...props} min="1900-01-01" placeholder="1970-01-01" />
  </div>
);

export default CustomDate;