using AutoMapper;
using BrewingBuddies_Entitys;
using BrewingBuddies_Entitys.Dtos.Requests;

namespace BrewingBuddies.MappingProfiles
{
    public class RequestToDomain : Profile 
    {
        public RequestToDomain() 
        {
            CreateMap<CreateUserRequest, LeagueUserEntity>()
                .ForMember(
                dest => dest.UserName,
                opt => opt.MapFrom(src => src.UserName))

                .ForMember(
                dest => dest.Status,
                opt => opt.MapFrom(src => 1)
                )
                .ForMember(
                dest => dest.UpdateDate,
                opt => opt.MapFrom(src => DateTime.UtcNow)
                )
                .ForMember(
                dest => dest.AddedDate,
                opt => opt.MapFrom(src => DateTime.UtcNow)
                );

            CreateMap<UpdateUserRequest, LeagueUserEntity>()
               .ForMember(
               dest => dest.UserName,
               opt => opt.MapFrom(src => src.UserName))

               .ForMember(
               dest => dest.UpdateDate,
               opt => opt.MapFrom(src => DateTime.UtcNow)
               );



        }
    }
}
