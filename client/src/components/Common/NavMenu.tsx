import { Fragment, useContext } from 'react';
import { Disclosure, Menu, Transition } from '@headlessui/react';
import { MenuIcon, FireIcon, XIcon, MoonIcon, SunIcon } from '@heroicons/react/solid';
import { Link, resolvePath, matchPath } from 'react-router-dom';
import { combineClassNames } from '../../services';
import { useTheme } from '../../hooks';
import { FadeInOut, GroupSelector, SmartLink, Switch, ThumbIcon, WideIcon } from '.'
import { useLocation } from 'react-router';
import { AuthContext } from '../../contexts';

interface INavMenuProps {
  links?: {
    name: string,
    href: string
  }[]
  hideGroupSelector?: boolean
}

const NavMenu = ({ links = [], hideGroupSelector }: INavMenuProps) => {

  const { user, logout, isAdmin } = useContext(AuthContext)
  const { theme, setTheme } = useTheme()
  const location = useLocation()

  const toggleDarkTheme = () => {
    theme === 'dark' ? setTheme('light') : setTheme('dark')
  }

  return (
    <Disclosure as="nav" className="mt-2 sm:mb-4">
      {({ open }) => (
        <>
          <div className="mx-auto px-2 sm:px-6 lg:px-8">
            <div className="flex flex-1 items-center justify-between h-16">
              <div className="flex flex-1 items-center justify-between">
                {/* Mobile menu button */}
                <Disclosure.Button className="lg:hidden rounded-md text-gray-400 hover:text-white hover:bg-gray-700 focus:outline-none focus:ring-2 focus:ring-inset focus:ring-white">
                  <span className="sr-only">Open main menu</span>
                  {open ? (
                    <XIcon className="block h-8 w-8" aria-hidden="true" />
                  ) : (
                    <MenuIcon className="block h-8 w-8" aria-hidden="true" />
                  )}
                </Disclosure.Button>
                {/* Brand Logo */}
                <div>
                  <Link to="/" className="flex-shrink-0 flex space-x-2 items-center transform hover:scale-102 transition-transform">
                    <ThumbIcon
                      className="h-10 w-10 dark:text-white"
                      alt="tiebreaker"
                    />
                    <WideIcon
                      className="hidden dark:text-white md:block w-36"
                      alt="tiebreaker"
                    />
                  </Link>
                </div>
              </div>
              <div className="items-center hidden lg:flex divide-x divide-blue-400 dark:divide-gray-700">
                <div className="sm:ml-6">
                  <div className="flex ">
                    {links.map((item) => (
                      <Link
                        key={item.name}
                        to={item.href}
                        className={combineClassNames(
                          matchPath(resolvePath(item.href).pathname, location.pathname) !== null && 'text-blue-500',
                          'px-3 rounded-md font-bold tracking-wide hover:text-blue-500 transition-colors'
                        )}
                        aria-current={matchPath(resolvePath(item.href).pathname, location.pathname) !== null && 'page'}
                      >
                        {item.name}
                      </Link>
                    ))}
                  </div>
                </div>

                <div className="hidden md:flex">
                  {/* Group selector */}
                  {!hideGroupSelector && (
                    <GroupSelector />
                  )}
                </div>

                <div className='hidden md:flex justify-center items-center pl-3 py-1'>
                  {/* Dark Mode toggle */}
                  <Switch enabled={theme === 'dark'} setEnabled={() => toggleDarkTheme()} description="theme" IconEnabled={MoonIcon} IconDisabled={SunIcon} />

                  {/* Profile dropdown */}
                  <Menu as="div" className="ml-3 relative">
                    <div>
                      <Menu.Button className="flex text-sm rounded-full focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-800 focus:ring-white">
                        <span className="sr-only">Open user menu</span>
                        <FireIcon className="w-8 h-auto text-gray-300 hover:text-blue-800 dark:hover:text-blue-500 transition-all" />
                      </Menu.Button>
                    </div>
                    <Transition
                      as={Fragment}
                      enter="transition ease-out duration-100"
                      enterFrom="transform opacity-0 scale-95"
                      enterTo="transform opacity-100 scale-100"
                      leave="transition ease-in duration-75"
                      leaveFrom="transform opacity-100 scale-100"
                      leaveTo="transform opacity-0 scale-95"
                    >
                      <Menu.Items className="origin-top-right absolute right-0 mt-2 w-48 rounded-md shadow-lg py-1 bg-white ring-1 ring-black ring-opacity-5 focus:outline-none z-50">
                        <div className="px-4 py-2 font-semibold tracking-wide text-color-header">
                          {user?.username}
                        </div>
                        {isAdmin() && (
                          <Menu.Item>
                            {({ active }) => (
                              <Link
                                to="/adminpanel"
                                className={combineClassNames(active ? 'bg-gray-100' : '', 'block w-full text-left px-4 py-2 text-sm text-purple-700')}
                              >
                                Admin Panel
                              </Link>
                            )}
                          </Menu.Item>
                        )}

                        <Menu.Item>
                          {({ active }) => (
                            <button
                              className={combineClassNames(active ? 'bg-gray-100' : '', 'w-full text-left px-4 py-2 text-sm text-gray-700')}
                            >
                              Your Profile
                            </button>
                          )}
                        </Menu.Item>
                        <Menu.Item>
                          {({ active }) => (
                            <button
                              className={combineClassNames(active ? 'bg-gray-100' : '', 'w-full text-left px-4 py-2 text-sm text-gray-700')}
                            >
                              Settings
                            </button>
                          )}
                        </Menu.Item>
                        <Menu.Item>
                          {({ active }) => (
                            <button
                              onClick={() => logout()}
                              className={combineClassNames(active ? 'bg-gray-100' : '', 'w-full text-left px-4 py-2 text-sm text-gray-700')}
                            >
                              Sign out
                            </button>
                          )}
                        </Menu.Item>
                      </Menu.Items>
                    </Transition>
                  </Menu>
                </div>
              </div>
            </div>
          </div>
          <FadeInOut>
            <Disclosure.Panel className="lg:hidden h-screen w-screen inset-0 bg-white dark:bg-black fixed z-50 mx-auto px-2 sm:px-6 overflow-y-hidden">
              <div className="flex items-center justify-between h-16 mt-2">
                <Disclosure.Button className="items-center rounded-md text-gray-400 hover:text-white hover:bg-gray-700 focus:outline-none focus:ring-2 focus:ring-inset focus:ring-white">
                  <span className="sr-only">Close main menu</span>
                  <XIcon className="block h-8 w-8" aria-hidden="true" />
                </Disclosure.Button>
                <Disclosure.Button as={SmartLink} to="/dashboard">
                  <WideIcon
                    className="dark:text-white w-36"
                    alt="tiebreaker"
                  />
                </Disclosure.Button>
              </div>
              <div className="flex border-b-2 border-gray-800/20 dark:border-gray-200/20 pb-2 mb-6">
                {/* Group selector */}
                {!hideGroupSelector && (
                  <GroupSelector fillContainer />
                )}
              </div>
              <div className="border-b-2 border-gray-800/20 dark:border-gray-200/20 pb-2 mb-6">
                {links.map((item, i) => (
                  <Disclosure.Button
                    key={i}
                    as={Link}
                    to={item.href}
                    className={combineClassNames(
                      "flex px-3 py-2 mb-2 text-xl font-bold tracking-wide justify-between items-center",
                      matchPath(resolvePath(item.href).pathname, location.pathname) !== null && "text-blue-500 after:w-3 after:h-3 after:bg-blue-500 after:rounded-full",
                    )}
                  >
                    {item.name}
                  </Disclosure.Button>
                ))}
              </div>
              <div className="flex justify-end">
                {/* Dark Mode toggle */}
                <Switch enabled={theme === 'dark'} setEnabled={() => toggleDarkTheme()} description="theme" IconEnabled={MoonIcon} IconDisabled={SunIcon} />
              </div>
            </Disclosure.Panel>
          </FadeInOut>
        </>
      )
      }
    </Disclosure>
  )
}

export default NavMenu