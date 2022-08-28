using Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL
{
    public class GroupRepository : GenericRepository<Group>
    {
        public GroupRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override Task<List<Group>> ReadByCondition(Expression<Func<Group, bool>> condition)
        {
            return Task.Run(() => _context.Set<Group>().Where(condition).Include(g => g.GroupUsers).ToList());
        }
    }
}
