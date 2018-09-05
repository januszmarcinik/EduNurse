using System.Linq;
using AutoMapper;
using EduNurse.Auth.AzureTableStorage;
using EduNurse.Auth.Entities;

namespace EduNurse.Auth
{
    internal class AuthMappings : Profile
    {
        public AuthMappings()
        {
            CreateMap<User, AtsUser>()
                .ForMember(x => x.PartitionKey, e => e.UseValue(AtsUsersContext.UsersPartitionKey))
                .ForMember(x => x.RowKey, e => e.MapFrom(m => m.Id))
                .ForMember(x => x.Roles, e => e.ResolveUsing(m => string.Join(",", m.Roles.Select(r => (int)r))))
                .ForMember(x => x.Timestamp, e => e.Ignore())
                .ForMember(x => x.ETag, e => e.UseValue("*"));
        }
    }
}
