using Auth.API.Helper;
using Auth.API.Model;
using Auth.DataInfra.DataContext;
using Auth.DataInfra.Entity;
using Auth.DataInfra.Repository;
using AutoMapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Services
{
    /// <summary>
    /// Service implementation
    /// </summary>
    public class AuthProviderServices : IAuthProviderService
    {
        private AuthenticationContext _context;
        private IAuthRepository _authRepository;
        private IMapper _mapper;
        //private ILogger _logger;

        public AuthProviderServices(AuthenticationContext context, IAuthRepository authRepository, IMapper mapper)
        {
            _context = context;
            _authRepository = authRepository;
            _mapper = mapper;
           
        }       

        public async Task<IEnumerable<AuthEntity>> AllLoginDetailsAsync()
        {                       
            var output =  await _authRepository.GetAllLoginsAsync();            
            //var result = _mapper.Map<List<LoginDetails>>(output);
            return output;
        }

        public void DeleteUser(Login loginModel)
        {
            var userDetails = _authRepository.GetLoginsWithDetailsAsync(loginModel.Email);
            
            if (userDetails.Result != null)
            {
                _authRepository.DeleteLogins(userDetails.Result);
                _authRepository.Commit();
            }
        }

        public Login LoginUser(Login loginModel)
        {
            if (loginModel == null )
                return null;

            var user = _authRepository.GetLoginsWithDetailsAsync(loginModel.Email);

            // check if username exists
            if (user.Result == null)
                return null;

            // check if password is correct
            if (!string.Equals(user.Result.Password,loginModel.Password))
                return null;

            // authentication successful
            var newuser = _mapper.Map<Login>(user.Result);

            //Insert loginIndex details
            var loginHistory = user.Result.LoginIndex;
            var currentTine = DateTime.UtcNow.ToString() + "$" + loginHistory;
            user.Result.LoginIndex = currentTine;
            _authRepository.UpdateLogins(user.Result);
            _authRepository.Commit();

            return newuser;
        }

        public Login RegisterUser(Login registrationModel)
        {
            //_logger.Information("Inside  registration service for, {Name}!", registrationModel.Email);
            // validation
            if (string.IsNullOrWhiteSpace(registrationModel.Password) || string.IsNullOrWhiteSpace(registrationModel.Email))
                throw new AppException("Password is required");

            if (_authRepository.GetLoginsByIdAsync(registrationModel.Email).Result)
                throw new AppException("Username \"" + registrationModel.Email + "\" is already taken");


            var authEntity = _mapper.Map<AuthEntity>(registrationModel);
            authEntity.LoginIndex = DateTime.Now.ToString();

            _authRepository.CreateLogins(authEntity);
            _authRepository.Commit();

            var loginModel = _mapper.Map<Login>(authEntity);
            return loginModel;
        }

        public void UpdateUser(Login loginModel)
        {
            var user =  _authRepository.GetLoginsWithDetailsAsync(loginModel.Email).Result;

            if (user == null)
                throw new AppException("User not found");

            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(loginModel.Email) && loginModel.Email != user.Email)
            {
                // throw error if the new username is already taken
                if (_authRepository.GetLoginsByIdAsync(loginModel.Email).Result)
                    throw new AppException("Username " + loginModel.Email + " is already taken");

                user.Email = loginModel.Email;
            }            

            // update password if provided
            if (!string.IsNullOrWhiteSpace(loginModel.Password))
            {               
                user.Password = loginModel.Password;               
            }

            _authRepository.Update(user);
            _authRepository.Commit();
        }        

        
    }
}
