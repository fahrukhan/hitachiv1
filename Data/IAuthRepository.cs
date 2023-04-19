using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hitachiv1.Data
{
    public interface IAuthRepository
    {
        Task<Response<int>> Register(User user, string password);
        Task<Response<string>> Login(string username, string password);
        Task<bool> UserExist(string username);
        Response<string> Test();
    }
}