using Core;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class GroupUserRepository : GenericRepository<GroupUser>
    {
        public GroupUserRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
