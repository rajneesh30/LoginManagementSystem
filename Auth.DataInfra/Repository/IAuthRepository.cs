using Auth.DataInfra.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Auth.DataInfra.Repository
{
    /// <summary>
    /// Exposed auth related operations
    /// </summary>
    public interface IAuthRepository : IRepositoryBase<AuthEntity>
    {
        Task<IEnumerable<AuthEntity>> GetAllLoginsAsync();
        Task<bool> GetLoginsByIdAsync(string email);
        Task<AuthEntity> GetLoginsWithDetailsAsync(string email);
        void CreateLogins(AuthEntity authEntity);
        void UpdateLogins(AuthEntity authEntity);
        void DeleteLogins(AuthEntity authEntity);
        void Commit();
    }
}
