using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Onym.Models;

namespace Onym.Services
{
    public interface IUserControlService
    {
        byte[] GetPasswordHash(string userPassword, int userPasswordSalt);
    }
}