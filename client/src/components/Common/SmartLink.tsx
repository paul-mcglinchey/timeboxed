import { useResolvedPath, useMatch, Link, PathMatch } from 'react-router-dom';
import { IChildrenProps, IProps } from '../../models';

interface ISmartLinkProps extends IProps, IChildrenProps {
  to: string,
  className: (match: PathMatch<string> | null) => string
}

const SmartLink = ({ className, children, to, ...props }: ISmartLinkProps) => {

  let resolved = useResolvedPath(to);
  let match: PathMatch<string> | null = useMatch({ path: resolved.pathname, end: true });

  return (
    <Link
      to={to}
      {...props}
      className={className(match)}
    >
      {children}
    </Link>
  )
}

export default SmartLink;