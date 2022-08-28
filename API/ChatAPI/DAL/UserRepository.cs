using Core;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<User> Read(int id)
        {
            User user = await _context.Set<User>().FirstAsync(u => u.Id == id);

            _context.Set<GroupUser>().Include(gu => gu.Group).Include(gu => gu.User).Load();

            return user;
        }
    }
}
