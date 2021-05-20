using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignarRWebAPIDemo.AuthData
{
    public interface IAuthenticate
    {
        Task<TokenModel> AuthenticateUser(LoginModel login);
    }
}
