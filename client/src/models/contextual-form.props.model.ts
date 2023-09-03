import { ReactNode } from "react";

export interface IContextualFormProps {
  ContextualSubmissionButton: (
    content?: string | undefined,
    actions?: () => Promise<void> | void,
    submissionGate?: boolean,
    isLoading?: boolean
  ) => ReactNode
}