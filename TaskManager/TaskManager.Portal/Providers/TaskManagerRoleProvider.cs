using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using TaskManager.Portal.BLL.Servicies;

namespace TaskManager.Portal.Providers
{
    public class TaskManagerRoleProvider : RoleProvider
    {
        private AuthService authService;
        public TaskManagerRoleProvider()
        {
            authService = new AuthService();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            authService.CreateUserGroupAsync(roleName, roleName).Wait();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            authService.RemoveUserGroup(roleName).Wait();
            return true;
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return authService.CheckUserInRole(username, roleName).GetAwaiter().GetResult();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}