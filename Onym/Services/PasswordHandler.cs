using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using IConfiguration = Castle.Core.Configuration.IConfiguration;

namespace Onym.Services
{
    public class PasswordHandler : IUserControlService
    {
        private const string GlobalSalt = "Y540X992";

        byte[] IUserControlService.GetPasswordHash(string userPassword, int userPasswordSalt)
        {
            var saltyPassword =
                Encoding.UTF8.GetBytes(string.Concat(GlobalSalt, userPassword,
                    userPasswordSalt.ToString()));
            var hashedPassword = new SHA512CryptoServiceProvider().ComputeHash(saltyPassword);
            return hashedPassword;
        }
    }
}