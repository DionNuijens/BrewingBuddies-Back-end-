using AutoMapper;
using BrewingBuddies_Entitys;
using BrewingBuddies_Entitys.Dtos.Requests;

namespace BrewingBuddies.MappingProfiles
{
    public class RequestToDomain : Profile 
    {
        public RequestToDomain() 
        {
            CreateMap<CreateLeagueUserRequest, LeagueUserEntity>()
                .ForMember(
                dest => dest.UserName,
                opt => opt.MapFrom(src => src.UserName))

                .ForMember(
                dest => dest.AccountId,
                opt => opt.MapFrom(src => src.AccountId))

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

            CreateMap<UpdateLeagueUserRequest, LeagueUserEntity>()
               .ForMember(
               dest => dest.UserName,
               opt => opt.MapFrom(src => src.UserName))

                .ForMember(
                dest => dest.AccountId,
                opt => opt.MapFrom(src => src.AccountId))

               .ForMember(
               dest => dest.UpdateDate,
               opt => opt.MapFrom(src => DateTime.UtcNow)
               );

            CreateMap<CreateRegistrationRequest, UserEntity>()
                .ForMember(
                    dest => dest.naam,
                    opt => opt.MapFrom(src => src.naam))

                .ForMember(
                    dest => dest.email,
                    opt => opt.MapFrom(src => src.email))

                .ForMember(
                    dest => dest.hash,
                    opt => opt.MapFrom(src => src.hash))

                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(src => 1))

                .ForMember(
                    dest => dest.UpdateDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow))

                .ForMember(
                    dest => dest.AddedDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow));




        }
    }
}
