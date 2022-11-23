import { Fragment } from 'react';
import { Disclosure, Menu, Transition } from '@headlessui/react';
import { MenuIcon, FireIcon, XIcon, MoonIcon, SunIcon } from '@heroicons/react/solid';
import { Link, PathMatch } from 'react-router-dom';
import { combineClassNames } from '../../services';
import { useAuthService, useTheme } from '../../hooks';
import { GroupSelector, SmartLink, Switch, ThumbIcon, WideIcon } from '.';

interface INavMenuProps {
  links?: {
    name: string,
    href: string
  }[]
  hideGroupSelector?: boolean
}

const NavMenu = ({ links = [], hideGroupSelector }: INavMenuProps) => {

  const { user, logout, isAdmin } = useAuthService()
  const { theme, setTheme } = useTheme()

  const toggleDarkTheme = () => {
    theme === 'dark' ? setTheme('light') : setTheme('dark')
  }

  return (
    <Disclosure as="nav" className="mt-2 mb-4">
      {({ open, close }) => (
        <>
          <div className="mx-auto px-2 sm:px-6 lg:px-8">
            <div className="relative flex items-center justify-between h-16">
              <div className="absolute inset-y-0 left-0 flex items-center sm:hidden">
                {/* Mobile menu button*/}
                <Disclosure.Button className="inline-flex items-center justify-center p-2 rounded-md text-gray-400 hover:text-white hover:bg-gray-700 focus:outline-none focus:ring-2 focus:ring-inset focus:ring-white">
                  <span className="sr-only">Open main menu</span>
                  {open ? (
                    <XIcon className="block h-6 w-6" aria-hidden="true" />
                  ) : (
                    <MenuIcon className="block h-6 w-6" aria-hidden="true" />
                  )}
                </Disclosure.Button>
              </div>
              <div className="flex-1 flex items-center justify-center sm:items-stretch sm:justify-start">
                <Link to="/" className="flex-shrink-0 flex space-x-2 items-center transform hover:scale-102 transition-transform">
                  <ThumbIcon
                    className="h-10 w-10 dark:text-white"
                    alt="tiebreaker"
                  />
                  <WideIcon
                    className="hidden dark:text-white lg:block w-36"
                    alt="tiebreaker"
                  />
                </Link>
              </div>
              <div className="flex items-center divide-x divide-blue-400 dark:divide-gray-700">
                <div className="hidden sm:block sm:ml-6">
                  <div className="flex ">
                    {links.map((item) => (
                      <SmartLink
                        key={item.name}
                        to={item.href}
                        className={(match: PathMatch<string> | null): string => (
                          combineClassNames(match ? 'text-blue-500' : '',
                            'px-3 rounded-md font-bold tracking-wide hover:text-blue-500 transition-colors'
                          )
                        )}
                        aria-current={(match: PathMatch<string> | null): string | undefined => match ? 'page' : undefined}
                      >
                        {item.name}
                      </SmartLink>
                    ))}
                  </div>
                </div>

                <div>
                  {/* Group selector */}
                  {!hideGroupSelector && (
                    <GroupSelector />
                  )}
                </div>

                <div className='flex justify-center items-center pl-3 py-1'>
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

          <Disclosure.Panel className="sm:hidden">
            <div className="px-2 pt-2 pb-3 space-y-1">
              {links.map((item) => (
                <SmartLink
                  key={item.name}
                  to={item.href}
                  onClick={() => close()}
                  className={(match: PathMatch<string> | null): string =>
                    combineClassNames(
                      match
                        ? 'bg-gray-900 text-white'
                        : 'text-gray-300 hover:bg-gray-700 hover:text-white',
                      'block px-3 py-2 rounded-md text-base font-medium'
                    )}
                  aria-current={(match: PathMatch<string> | null): string | undefined => match ? 'page' : undefined}
                >
                  {item.name}
                </SmartLink>
              ))}
            </div>
          </Disclosure.Panel>
        </>
      )
      }
    </Disclosure>
  )
}

export default NavMenu