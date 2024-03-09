using Microsoft.EntityFrameworkCore;
using MiniRedSocial.Core.Domain.Common;
using MiniRedSocial.Core.Domain.Entities;


namespace MiniRedSocial.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        {
        
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Hilo> Hilos { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedby = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
            
        //    modelBuilder.Entity<Message>()
        //        .HasOne(uf => uf.User)
        //        .WithMany(u => u.Messages)
        //        .HasForeignKey(uf => uf.UserId)
        //        .OnDelete(DeleteBehavior.Restrict);//Antes de eliminar a un usuario se deben eliminar todos sus mensajes

        //    modelBuilder.Entity<Hilo>()
        //        .HasOne(uf => uf.User)
        //        .WithMany(u => u.Hilos)
        //        .HasForeignKey(uf => uf.UserId)
        //        .OnDelete(DeleteBehavior.Restrict);//Antes de eliminar a un usuario se deben eliminar todos sus mensajes en hilos

        //}
    }
    
}
