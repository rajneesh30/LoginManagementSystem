using Auth.DataInfra.DataContext;
using Auth.DataInfra.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.DataInfra.Repository
{
    /// <summary>
    /// It will imlement auth related operations
    /// </summary>
    public class AuthRepository : RepositoryBase<AuthEntity>, IAuthRepository
    {
        public AuthRepository(AuthenticationContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateLogins(AuthEntity authEntity)
        {
            Create(authEntity);
        }

        public void DeleteLogins(AuthEntity authEntity)
        {
            Delete(authEntity);
        }

        public void UpdateLogins(AuthEntity authEntity)
        {
            Update(authEntity);
        }

        public void Commit()
        {
            Save();
        }
        public async Task<IEnumerable<AuthEntity>> GetAllLoginsAsync()
        {
            return await FindAll()
            .OrderBy(login => login.Email)
            .ToListAsync();
        }

        public async Task<bool> GetLoginsByIdAsync(string email)
        {
            var result = await FindByCondition(login => login.Email.Equals(email)).FirstOrDefaultAsync();
            return result != null ? result.Email.Equals(email) : false;
            
        }

        public async Task<AuthEntity> GetLoginsWithDetailsAsync(string email)
        {
            return await FindByCondition(login => login.Email.Equals(email))
            .FirstOrDefaultAsync();
        }

        
    }
}
