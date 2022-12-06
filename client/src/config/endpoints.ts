console.log(process.env)

const BASE_API_URL = process.env['REACT_APP_API_URL'] || 'http://localhost:3001/api/';
const DOTNET_API_URL = process.env['NODE_ENV'] === 'development' ? 'http://localhost:7193/api/' : process.env['REACT_APP_DOTNET_API_URL']

export const endpoints = {
    "origin"    : BASE_API_URL,

    "metainfo"  : (groupId?: string) => DOTNET_API_URL + `metainfo${groupId ? `?${groupId}` : ''}`,

    "login"         : DOTNET_API_URL + `users/login`,
    "signup"        : DOTNET_API_URL + `users/signup`,
    "authenticate"  : DOTNET_API_URL + `users/authenticate`,

    "groups"        : DOTNET_API_URL + 'groups',
    "groupinvites"  : DOTNET_API_URL + 'groupinvites',
    "group"         : (groupId: string) => DOTNET_API_URL + `groups/${groupId}`,
    "joingroup"     : (groupId: string) => DOTNET_API_URL + `groups/${groupId}/join`,
    "leavegroup"    : (groupId: string) => DOTNET_API_URL + `groups/${groupId}/leave`,
    
    "applications": DOTNET_API_URL + `applications`,
    "application": (applicationId: number) => DOTNET_API_URL + `applications/${applicationId}`,

    "clients"   : (groupId: string) => DOTNET_API_URL + `groups/${groupId}/clients`,
    "client"    : (clientId: string, groupId: string) => DOTNET_API_URL + `groups/${groupId}/clients/${clientId}`, 
    
    "sessions"  : (clientId: string, groupId: string) => DOTNET_API_URL + `groups/${groupId}/clients/${clientId}/sessions`,
    "session"   : (clientId: string, groupId: string, sessionId: string) => DOTNET_API_URL + `groups/${groupId}/clients/${clientId}/sessions/${sessionId}`,
    
    "rotas"     : (groupId: string) => DOTNET_API_URL + `groups/${groupId}/rotas`,
    "rota"      : (rotaId: string, groupId: string) => DOTNET_API_URL + `groups/${groupId}/rotas/${rotaId}`,
    
    "schedules" : (rotaId: string, groupId: string) => DOTNET_API_URL + `groups/${groupId}/rotas/${rotaId}/schedules`,
    "schedule"  : (rotaId: string, groupId: string, scheduleId: string) => DOTNET_API_URL + `groups/${groupId}/rotas/${rotaId}/schedules/${scheduleId}`,

    "employees" : (groupId: string) => DOTNET_API_URL + `groups/${groupId}/employees`,
    "employee"  : (employeeId: string, groupId: string) => DOTNET_API_URL + `groups/${groupId}/employees/${employeeId}`,
    
    "user"              : (userId: string) => DOTNET_API_URL + `users/${userId}`,
    "userpreferences"   : (userId?: string) => DOTNET_API_URL + `users/${userId ? `${userId}/` : ''}preferences`,
    "groupusers"        : (groupId: string) => DOTNET_API_URL + `groups/${groupId}/users`,
    "groupuser"         : (groupId: string, userId: string) => DOTNET_API_URL + `groups/${groupId}/users/${userId}`,
    "invitegroupuser"   : (groupId: string) => DOTNET_API_URL + `groups/${groupId}/users/invite`,
    "uninvitegroupuser" : (groupId: string, userId: string) => DOTNET_API_URL + `groups/${groupId}/users/${userId}/uninvite`,
    "currentuser"       : DOTNET_API_URL + "users/current",
    
    "systemlistcollection"  : (listcollectionId: string) => BASE_API_URL + `listcollections/system/${listcollectionId}`,
    "systemlistcollections" : BASE_API_URL + "listcollections/system",

    "roles"                 : DOTNET_API_URL + "roles",
    "grouproles"            : (groupId: string) => DOTNET_API_URL + `roles?groupId=${groupId}`,

    "permissions"           : DOTNET_API_URL + "permissions",
    "permission"            : (permissionId: number) => DOTNET_API_URL + `permissions/${permissionId}`,
}