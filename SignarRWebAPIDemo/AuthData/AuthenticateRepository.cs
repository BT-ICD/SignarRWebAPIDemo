using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignarRWebAPIDemo.AuthData
{
    public class AuthenticateRepository: IAuthenticate
    {
        private UserManager<ApplicationUser> userManager;
        private TokenGenerator tokenGenerator;
        public AuthenticateRepository(UserManager<ApplicationUser> userManager, TokenGenerator tokenGenerator)
        {
            this.userManager = userManager;
            this.userManager.PasswordHasher = new CustomPasswordHasher();
            this.tokenGenerator = tokenGenerator;
        }
        public async Task<TokenModel> AuthenticateUser(LoginModel login)
        {
            var user = await userManager.FindByNameAsync(login.UserName);

            if (user != null && await userManager.CheckPasswordAsync(user, login.Password))
            {
                //Get roles for the user
                var roles = await userManager.GetRolesAsync(user);
                if (roles.Count == 1)
                {
                    var result = tokenGenerator.GenerateToken(user.UserName, roles[0]);
                    return result;
                }
                return null;
            }
            return null;
        }
    }
}
