using SignarRWebAPIDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignarRWebAPIDemo.AppRepository
{
    public interface IAppUser
    {
        Task<DataUpdateResponseDTO> CreateUserAsync(AppUser appUser);
        bool IsUserNameExist(String userName);
        List<AppUserDTOList> GetList();
    }
}
