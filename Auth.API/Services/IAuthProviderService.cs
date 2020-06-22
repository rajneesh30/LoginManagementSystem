using Auth.API.Model;
using Auth.DataInfra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Services
{
    /// <summary>
    /// Service to expose various login related operation
    /// </summary>
    public interface IAuthProviderService
    {
        public Login LoginUser(Login loginModel);
        public Login RegisterUser(Login registrationModel);
        public void UpdateUser(Login loginModel);
        public void DeleteUser(Login loginModel);
        public Task<IEnumerable<AuthEntity>> AllLoginDetailsAsync();
    }
}
