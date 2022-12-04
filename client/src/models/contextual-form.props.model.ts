import { ReactNode } from "react";

export interface IContextualFormProps {
  ContextualSubmissionButton: (
    content?: string | undefined,
    actions?: (() => void)[] | undefined,
    submissionGate?: boolean,
    shouldClose?: boolean,
    isLoading?: boolean
  ) => ReactNode
}