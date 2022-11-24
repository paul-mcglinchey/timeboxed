export const dashboardLinks = [
  { name: 'Dashboard', href: '/dashboard' },
  { name: 'Groups', href: '/groups' }
]

export const clientLinks = dashboardLinks.concat([
  { name: 'Clients', href: '/clients/dashboard' },
]);

export const rotaLinks = dashboardLinks.concat([
  { name: 'Rotas', href: '/rotas/dashboard' },
  { name: 'Employees', href: '/rotas/employees' },
])