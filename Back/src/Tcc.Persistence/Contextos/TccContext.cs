using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Tcc.Domain;
using Tcc.Domain.Identity;
using Tcc.Persistence.Mappings;

namespace Tcc.Persistence.Contextos
{
    public class TccContext : IdentityDbContext<User, Role, Guid,
                                                       IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>, 
                                                       IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public TccContext(DbContextOptions<TccContext> options) : base(options) { }
        public DbSet<Associado> Associados { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<VistoriaImagem> VistoriaImagem { get; set; }

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

            modelBuilder.Entity<Associado>()
                .HasMany(e => e.Veiculos)
                .WithOne(rs => rs.Associado)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Veiculo>()
                .HasMany(e => e.ListaImagens)
                .WithOne(rs => rs.Veiculo)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.ApplyConfiguration(new AssociadoMap());
            modelBuilder.ApplyConfiguration(new VeiculoMap());
            modelBuilder.ApplyConfiguration(new VistoriaImagemMap());
        }
    }
}