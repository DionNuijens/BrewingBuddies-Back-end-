using AutoMapper;
using BrewingBuddies_Entitys.Dtos.Requests;
using BrewingBuddies_Entitys;
using BrewingBuddies_Entitys.Dtos.Responses;

namespace BrewingBuddies.MappingProfiles
{
    public class DomainToResponse : Profile
    {
        public DomainToResponse()
        {
            CreateMap<LeagueUserEntity, UserResponse>()
                .ForMember(
                dest => dest.UserName,
                opt => opt.MapFrom(src => src.UserName));


        }
    }
}
