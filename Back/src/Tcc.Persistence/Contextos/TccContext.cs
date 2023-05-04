using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tcc.Domain;
using Tcc.Domain.Identity;

namespace Tcc.Persistence.Contextos
{
    public class TccContext : IdentityDbContext<User, Role, int, 
                                                       IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, 
                                                       IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public TccContext(DbContextOptions<TccContext> options) : base(options) { }
        public DbSet<Associado> Associados { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole => 
                {
                    userRole.HasKey(ur => new { ur.UserId, ur.RoleId});

                    userRole.HasOne(ur => ur.Role)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired();

                    userRole.HasOne(ur => ur.User)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired();
                }
            );

            //modelBuilder.Entity<PalestranteEvento>()
            //    .HasKey(PE => new {PE.EventoId, PE.PalestranteId});

            modelBuilder.Entity<Associado>()
                .HasMany(e => e.Veiculos)
                .WithOne(rs => rs.Associado)
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Palestrante>()
            //    .HasMany(e => e.RedesSociais)
            //    .WithOne(rs => rs.Palestrante)
            //    .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.ApplyConfiguration(new AssociadoMap());
        }
    }
}