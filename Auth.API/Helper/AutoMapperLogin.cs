using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.API.Model;
using Auth.DataInfra.Entity;
using AutoMapper;

namespace Auth.API.Helper
{
    public class AutoMapperLogin : Profile
    {
        public AutoMapperLogin()
        {
            CreateMap<AuthEntity,Login>();
            CreateMap<Login, AuthEntity>();
            CreateMap<LoginDetails, AuthEntity>();
            CreateMap<IEnumerable<AuthEntity>, List<LoginDetails>>();
            CreateMap<List<LoginDetails>, IEnumerable<AuthEntity>>();
        }
    }
}
