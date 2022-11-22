import { useCallback, useEffect, useRef, useState } from "react";

const useStateCallback = <S>(initialState: S) => {
  const [state, setState] = useState<S>(initialState)
  const cbRef = useRef<((state: S) => void) | null>(null); // init mutable ref container for callbacks

  const setStateCallback = useCallback((state: React.SetStateAction<S>, cb: (state: S) => void = () => {}) => {
    cbRef.current = cb;
    setState(state);
  }, []);

  useEffect(() => {
    if (cbRef.current) {
      cbRef.current(state);
      cbRef.current = null; // reset callback after execution
    }
  }, [state]);

  return [state, setStateCallback] as const;
}

export default useStateCallback;