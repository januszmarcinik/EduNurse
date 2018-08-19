using AutoMapper;
using EduNurse.Authentication.AzureTableStorage;
using EduNurse.Authentication.Entities;

namespace EduNurse.Authentication
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
