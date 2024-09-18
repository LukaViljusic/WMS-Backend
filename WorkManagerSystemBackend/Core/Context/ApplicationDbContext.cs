using Microsoft.EntityFrameworkCore;
using WorkManagerSystemBackend.Core.Entities;

namespace WorkManagerSystemBackend.Core.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Space> Spaces { get; set; }
        public DbSet<UserSpace> UserSpaces { get; set; }
        public DbSet<WorkItemType> WorkItemTypes { get; set; }
        public DbSet<WorkItemState> WorkItemStates { get; set; }
        public DbSet<WorkItem> WorkItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>(entity => { entity.HasIndex(e => e.Email).IsUnique(); });

            modelBuilder.Entity<Space>()
                .HasOne(space => space.Users)
                .WithMany(users => users.Spaces)
                .HasForeignKey(space => space.UsersId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkItemType>()
                .HasOne(workitemtype => workitemtype.Space)
                .WithMany(space => space.WorkItemTypes)
                .HasForeignKey(workitemtype => workitemtype.SpaceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkItemState>()
                .HasOne(workitemstate => workitemstate.Space)
                .WithMany(space => space.WorkItemStates)
                .HasForeignKey(workitemstate => workitemstate.SpaceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkItem>()
                .HasOne(workitem => workitem.WorkItemType)
                .WithMany(workitemtype => workitemtype.WorkItems)
                .HasForeignKey(workitem => workitem.WorkItemTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserSpace>()
                .HasOne(us => us.Users)
                .WithMany(u => u.UserSpaces)
                .HasForeignKey(us => us.UsersId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserSpace>()
                .HasOne(us => us.Space)
                .WithMany(s => s.UserSpaces)
                .HasForeignKey(us => us.SpaceId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
