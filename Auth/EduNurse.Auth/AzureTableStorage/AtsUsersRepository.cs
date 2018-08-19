using System.Threading.Tasks;
using AutoMapper;
using EduNurse.Auth.Entities;
using Microsoft.WindowsAzure.Storage.Table;

namespace EduNurse.Auth.AzureTableStorage
{
    internal class AtsUsersRepository : IUsersRepository
    {
        private readonly AtsUsersContext _context;
        private readonly IMapper _mapper;

        public AtsUsersRepository(AtsUsersContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _context.GetSingleAsync(new TableQuery<AtsUser>()
                .Where(
                    TableQuery.CombineFilters(
                        TableQuery.GenerateFilterCondition(nameof(AtsUser.PartitionKey), QueryComparisons.Equal, AtsUsersContext.UsersPartitionKey),
                        TableOperators.And,
                        TableQuery.GenerateFilterCondition(nameof(AtsUser.Email), QueryComparisons.Equal, email)
                    )
                )
            );

            return user?.ToDomainModel();
        }

        public async Task AddAsync(User user)
        {
            var operation = TableOperation.Insert(_mapper.Map<AtsUser>(user));
            await _context.Table.ExecuteAsync(operation);
        }
    }
}
