using Sep3Tier3WithAuth.Entities;
using Sep3Tier3WithAuth.Models;
using AutoMapper;

namespace Sep3Tier3WithAuth.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Fisher, FisherModel>();
            CreateMap<Fisher, RegisterModel>();
            CreateMap<Fisher, UpdateModel>();
            CreateMap<Administrator,AddAdminModel>();
            CreateMap<Fisher, FisherInfoForMatches>();
        }
    }
}
