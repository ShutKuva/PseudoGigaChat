using Core;
using Group = Core.Group;
using Microsoft.EntityFrameworkCore;
using Bogus;

namespace DAL
{
    public class ChatContext : DbContext
    {
        private readonly Hasher _hasher;

        public DbSet<Group> Groups { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }

        public ChatContext(DbContextOptions<ChatContext> options, Hasher hasher) : base(options)
        {
            _hasher = hasher;
        }

        public ChatContext()
        {
            _hasher = new Hasher();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=ZHEKA\\SQLEXPRESS;Database=ChatDb;Trusted_Connection=True;");
            builder.EnableDetailedErrors();
            builder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            int numberOfUsers = 100;
            int numberOfGroups = 10;

            int mcounter = 1;
            var messageFaker = new Faker<Message>("uk")
                .RuleFor(m => m.Id, _ => mcounter++)
                .RuleFor(m => m.Text, faker => faker.Lorem.Text())
                .RuleFor(m => m.Created, faker => faker.Date.Past());

            int gcounter = 1;
            var groupFaker = new Faker<Group>("uk")
                .RuleFor(g => g.Name, faker => faker.Name.JobTitle())
                .RuleFor(g => g.Id, _ => gcounter++);

            int ucounter = 1;
            var userFaker = new Faker<User>("uk")
                .RuleFor(u => u.Id, _ => ucounter++)
                .RuleFor(u => u.Name, faker => faker.Name.FirstName())
                .RuleFor(u => u.Password, faker =>
                {
                    string result = faker.Internet.Password();
                    return _hasher.HashStringSha(result);
                });

            List<User> users = userFaker.Generate(numberOfUsers);
            List<Group> groups = groupFaker.Generate(numberOfGroups);
            List<GroupUser> groupUsers = new List<GroupUser>();
            List<Message> messages = new List<Message>();

            var rand = new Random();
            IEnumerable<User> usersInGroup;
            List<GroupUser> groupUsersTemp;

            int groupUsersCounter = 1;

            foreach (Group group in groups)
            {
                int from = rand.Next(0, numberOfUsers);
                int to = rand.Next(numberOfUsers - from, numberOfUsers + 1);

                usersInGroup = users.Skip(from).Take(to - from);
                groupUsersTemp = new List<GroupUser>();

                GroupUser temp;
                List<Message> messagesTemp = new List<Message>();

                foreach (User user in usersInGroup)
                {
                    temp = new GroupUser() { Id = groupUsersCounter++, GroupId = group.Id, UserId = user.Id };

                    groupUsersTemp.Add(temp);

                    if(user.GroupUsers == null)
                    {
                        user.GroupUsers = new List<GroupUser>();
                    }

                    messagesTemp = messageFaker.Generate(rand.Next(10));
                    messagesTemp.ForEach(m =>
                    {
                        m.UserId = user.Id;
                        m.GroupId = group.Id;
                    });

                    messages.AddRange(messagesTemp);
                }

                groupUsers.AddRange(groupUsersTemp);
            }

            users.Add(new User
            {
                Id = ucounter,
                Name = "test",
                Password = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08"
            });

            foreach (Group group in groups)
            {
                groupUsers.Add(new GroupUser { Id = groupUsersCounter++,GroupId = group.Id, UserId = ucounter});
            }

            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Group>().HasData(groups);
            modelBuilder.Entity<GroupUser>().HasData(groupUsers);
            //modelBuilder.Entity<User>().OwnsMany(u => u.GroupUsers).HasData(groupUsers);
            //modelBuilder.Entity<Group>().OwnsMany(g => g.GroupUsers).HasData(groupUsers);
            modelBuilder.Entity<Message>().HasData(messages);
        }
    }
}
