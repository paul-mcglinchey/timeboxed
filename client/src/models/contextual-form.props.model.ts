import { ReactNode } from "react";

export interface IContextualFormProps {
  ContextualSubmissionButton: (
    content?: string | undefined, 
    actions?: (() => void)[] | undefined,
    submissionGate?: boolean
  ) => ReactNode
}