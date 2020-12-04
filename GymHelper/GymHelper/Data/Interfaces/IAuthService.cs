using GymHelper.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.Data.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginTo(string username, string password);
        Task<bool> Register(User user);
    }
}
