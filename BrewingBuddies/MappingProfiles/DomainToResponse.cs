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
            CreateMap<LeagueUserEntity, LeagueUserResponse>()
                .ForMember(
                dest => dest.UserName,
                opt => opt.MapFrom(src => src.UserName))

                .ForMember(
                dest => dest.AccountId,
                opt => opt.MapFrom(src => src.AccountId));


        }
    }
}
