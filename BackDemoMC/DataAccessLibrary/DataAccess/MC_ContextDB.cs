namespace DataAccessLibrary.DataAccess
{
    using System;
    using DataAccessLibrary.Enum;
    using DataAccessLibrary.Model;
    using Microsoft.EntityFrameworkCore;

    public class MC_ContextDb : DbContext
    {
        public MC_ContextDb(DbContextOptions<MC_ContextDb> options)
            : base(options)
        {
        }

        public DbSet<MigrationCenteres> MigrationCenteres { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UrAgent> UrAgent { get; set; }

        public DbSet<Agent> Agent { get; set; }

        public DbSet<Admin> Admin { get; set; }

        public DbSet<Buhgalter> Buhgalter { get; set; }

        public DbSet<UserMigrant> UserMigrant { get; set; }

        public DbSet<Migrant> Migrant { get; set; }

        public DbSet<Service> Service { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<OrderItem> OrderItem { get; set; }

        public DbSet<PayList> PayList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMigrant>().HasKey(x => new { x.UserId, x.MigrantId });

            modelBuilder.Entity<UserMigrant>()
                .HasOne(um => um.Migrant)
                .WithMany(u => u.UserMigrantes)
                .HasForeignKey(um => um.MigrantId);

            modelBuilder.Entity<UserMigrant>()
                .HasOne(um => um.UrAgent)
                .WithMany(u => u.UserMigrantes)
                .HasForeignKey(um => um.UserId);

            modelBuilder.Entity<Role>().HasData(new Role[]
            {
                new Role()
                    {
                    Id = (int)EnumRole.Admin,
                    Name = EnumRole.Admin.ToString(),
                    },
                new Role()
                    {
                    Id = (int)EnumRole.Jurperson,
                    Name = EnumRole.Jurperson.ToString(),
                    },
                new Role() { Id = (int)EnumRole.Agent, Name = EnumRole.Agent.ToString() },
            });

            modelBuilder.Entity<Service>().HasData(new Service[]
            {
                new Service() { Id = 1, Name = "Оплата НДФЛ", Cost = 400, IsDelete = false },
                new Service() { Id = 2, Name = "Мед. страхование", Cost = 600, IsDelete = false },
                new Service() { Id = 3, Name = "Оформление ВНЖ", Cost = 1100, IsDelete = false },
                new Service() { Id = 4, Name = "Мед.Освидетельствование", Cost = 1000, IsDelete = false },
                new Service() { Id = 5, Name = "Экзамен русского языка", Cost = 800, IsDelete = false },
                new Service() { Id = 6, Name = "Базовый экзамен русского языка", Cost = 600, IsDelete = false },
                new Service() { Id = 7, Name = "Заключение труд. договора", Cost = 600, IsDelete = false },
            });

            modelBuilder.Entity<Admin>().HasData(new Admin() { Id = 1, Name = "admin", RoleId = 1 });
        }
    }
}