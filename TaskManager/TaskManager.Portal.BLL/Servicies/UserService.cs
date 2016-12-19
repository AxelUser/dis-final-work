using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DAL.Entities;
using TaskManager.Portal.BLL.DTO;

namespace TaskManager.Portal.BLL.Servicies
{
    public class UserService
    {
        const string SALT_FOR_DEBUG = "saltfordebug";
        public bool IsUserCredentialsCorrect(LoginViewModel model)
        {
            using(TaskManagerContext db = new TaskManagerContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Login == model.Login
                    && u.PasswordHash == GetPasswordHash(model.Password));
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
