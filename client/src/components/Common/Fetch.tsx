import { memo } from "react";

const Fetch = ({ render, fetchOutput }: any) => {
  return render(fetchOutput);
}

export default memo(Fetch);