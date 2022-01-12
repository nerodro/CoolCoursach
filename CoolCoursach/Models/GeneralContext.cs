using Microsoft.EntityFrameworkCore;

namespace CoolCoursach.Models
{
    public class GeneralContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Facult> Facults { get; set; }
        public DbSet<Status> Statuss { get; set; }
        public GeneralContext(DbContextOptions<GeneralContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string moderRoleName = "moder";
            string userRoleName = "user";

            string adminlogin = "admin";
            string adminPass = "123";

            string moderlogin = "moder";
            string moderpass = "12";

            string GroupName = "И-1А";
            string GroupName2 = "И-2А";

            string FacultName = "Информатика";
            string FacultName2 = "Стройка";

            string StatusActive = "Учится";
            string StatusInactive = "Отчислен";
            string StatusFinished = "Выпустился";

            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role moderRole = new Role { Id = 2, Name = moderRoleName };
            Role userRole = new Role { Id = 3, Name = userRoleName };
            User adminUser = new User { Id = 1, Email =  adminlogin , Password = adminPass, RoleId = adminRole.Id};
            User moderUser = new User { Id = 2, Email = moderlogin , Password = moderpass , RoleId = moderRole.Id};
            Group firstgroup = new Group { Id = 1, Name = GroupName };
            Group secondgroup = new Group { Id = 2, Name = GroupName2 };
            Facult firstfacult = new Facult { Id = 1, Name = FacultName };
            Facult secondfacult = new Facult { Id = 2, Name = FacultName2 };
            Status statusActive = new Status { Id = 1, Name = StatusActive };
            Status statusInactive = new Status { Id = 2,Name = StatusInactive };
            Status statusFinished = new Status { Id = 3,Name = StatusFinished };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, moderRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser, moderUser });
            modelBuilder.Entity<Group>().HasData(new Group[] { firstgroup, secondgroup });
            modelBuilder.Entity<Facult>().HasData(new Facult[] { firstfacult, secondfacult });
            modelBuilder.Entity<Status>().HasData(new Status[] { statusActive, statusInactive, statusFinished });
            base.OnModelCreating(modelBuilder);
        }
    }
}
