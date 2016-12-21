using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DAL.Entities;
using TaskManager.Portal.BLL.DTO;

namespace TaskManager.Portal.BLL.Servicies
{
    public class AuthService
    {
        public enum Roles
        {
            Admin = 10,
            User = 20
        };

        const string SALT_FOR_DEBUG = "saltfordebug";
        public async Task<bool> IsUserCredentialsCorrectAsync(LoginViewModel model)
        {
            using(TaskManagerContext db = new TaskManagerContext())
            {
                var passwordHash = GetPasswordHash(model.Password);
                var user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email
                    && u.PasswordHash == passwordHash);
                return user != null;
            }
        }

        public async Task<bool> IsRegisteredUserExists(RegisterViewModel model)
        {
            using(TaskManagerContext db = new TaskManagerContext())
            {
                bool isUserExist = await db.Users.AnyAsync(u => u.Email == model.Email
                    || u.Email == model.Email);
                return isUserExist;
            }
        }

        public async Task RegisterUser(RegisterViewModel model)
        {
            using(TaskManagerContext db = new TaskManagerContext())
            {
                User newUser = new User()
                {
                    Email = model.Email,
                    PasswordHash = GetPasswordHash(model.Password),
                    FullName = model.FullName,
                    UserGroupId = (int) Roles.User
                };
                db.Users.Add(newUser);
                await db.SaveChangesAsync();
            }
        }

        public async Task CreateUserGroupAsync(string name, string caption)
        {
            using(TaskManagerContext db = new TaskManagerContext())
            {
                if (! (await db.UserGroups.AnyAsync(us => us.Name == name)))
                {
                    db.UserGroups.Add(new UserGroup()
                    {
                        Name = name,
                        Caption = caption
                    });
                    await db.SaveChangesAsync();
                }                
            }
        }

        public async Task RemoveUserGroup(string name)
        {
            using(TaskManagerContext db = new TaskManagerContext())
            {
                var group = await db.UserGroups.SingleOrDefaultAsync(ug => ug.Name == name);
                if(group != null)
                {
                    db.UserGroups.Remove(group);
                    await db.SaveChangesAsync();
                }
            }
        }

        public async Task<bool> CheckUserInRole(string username, string group)
        {
            using(TaskManagerContext db = new TaskManagerContext())
            {
                var user = await db.Users.Include(u => u.UserGroup)
                    .SingleOrDefaultAsync(u => u.Email == username && u.UserGroup.Name == group);
                return user != null;
            }
        }

        private string GetPasswordHash(string password)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(SALT_FOR_DEBUG + password));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}
