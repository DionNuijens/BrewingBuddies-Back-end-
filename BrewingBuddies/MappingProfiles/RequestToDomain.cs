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
                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
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
               dest => dest.RiotId,
               opt => opt.MapFrom(src => src.RiotId))
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
                dest => dest.RiotId,
                opt => opt.MapFrom(src => src.RiotId))


               .ForMember(
               dest => dest.UpdateDate,
               opt => opt.MapFrom(src => DateTime.UtcNow)
               );

            CreateMap<UpdateRequestRequest, RequestEntity>()
                .ForMember(
                dest => dest.State,
                opt => opt.MapFrom(src => src.State))

                                .ForMember(
                dest => dest.challengerKDA,
                opt => opt.MapFrom(src => src.challengerKDA))

                .ForMember(
                dest => dest.defenderKDA,
                opt => opt.MapFrom(src => src.defenderKDA))

                .ForMember(
                dest => dest.winner,
                opt => opt.MapFrom(src => src.winner))


               .ForMember(
               dest => dest.UpdateDate,
               opt => opt.MapFrom(src => DateTime.UtcNow)
               );


            CreateMap<CreateRegistrationRequest, UserEntity>()
                 //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))


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
            
            CreateMap<CreateRiotEntityRequest, RiotEntity>()

                 .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))

                .ForMember(
                    dest => dest.summonerName,
                    opt => opt.MapFrom(src => src.summonerName))

                .ForMember(
                    dest => dest.tagLine,
                    opt => opt.MapFrom(src => src.tagLine))

                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(src => 1))

                .ForMember(
                    dest => dest.UpdateDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow))

                .ForMember(
                    dest => dest.AddedDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<CreateRequestRequest, RequestEntity>()
              //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))

             .ForMember(
                 dest => dest.State,
                 opt => opt.MapFrom(src => src.State))

             .ForMember(
                 dest => dest.challenger,
                 opt => opt.MapFrom(src => src.challenger))

             .ForMember(
                 dest => dest.defender,
                 opt => opt.MapFrom(src => src.defender))

             .ForMember(
                 dest => dest.challengerKDA,
                 opt => opt.MapFrom(src => src.challengerKDA))

             .ForMember(
                 dest => dest.defenderKDA,
                 opt => opt.MapFrom(src => src.defenderKDA))

             .ForMember(
                 dest => dest.winner,
                 opt => opt.MapFrom(src => src.winner))

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
