using AutoMapper;
using EduNurse.Auth.AzureTableStorage;
using EduNurse.Auth.Entities;

namespace EduNurse.Auth
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, AtsUser>()
                .ForMember(x => x.PartitionKey, e => e.UseValue(AtsUsersContext.UsersPartitionKey))
                .ForMember(x => x.RowKey, e => e.MapFrom(m => m.Id))
                .ForMember(x => x.Timestamp, e => e.Ignore())
                .ForMember(x => x.ETag, e => e.UseValue("*"));
        }
    }
}
