using Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL
{
    public class MessageRepository : GenericRepository<Message>
    {
        public MessageRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override Task<List<Message>> ReadByCondition(Expression<Func<Message, bool>> condition)
        {
            return Task.Run(() => _context.Set<Message>().Where(condition).Include(m => m.User).Include(m => m.Replied).ToList());
        }
    }
}
