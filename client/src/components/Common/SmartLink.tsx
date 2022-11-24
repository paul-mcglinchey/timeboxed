import { forwardRef, Ref } from 'react';
import { useResolvedPath, useMatch, Link, PathMatch } from 'react-router-dom';
import { IChildrenProps, IProps } from '../../models';

interface ISmartLinkProps extends IProps, IChildrenProps {
  to: string,
  className: (match: PathMatch<string> | null) => string
}

const SmartLink = forwardRef(({ className, children, to, ...props }: ISmartLinkProps, ref: Ref<HTMLAnchorElement>) => {

  let resolved = useResolvedPath(to);
  let match: PathMatch<string> | null = useMatch({ path: resolved.pathname, end: true });

  return (
    <Link
      ref={ref}
      to={to}
      {...props}
      className={className && className(match)}
    >
      {children}
    </Link>
  )
})

export default SmartLink;