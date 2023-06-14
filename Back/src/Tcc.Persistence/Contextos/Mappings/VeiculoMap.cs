using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tcc.Domain;

namespace Tcc.Persistence.Mappings
{
    public class VeiculoMap : IEntityTypeConfiguration<Veiculo>
    {
        public virtual void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.ToTable("Veiculo");

            builder.HasKey(x => x.Id);
            builder.Property(f => f.DataInclusao).IsRequired();
            builder.Property(f => f.DataAlteracao).IsRequired(false);
            builder.Property(f => f.RegistroAtivo).IsRequired();
        }
    }
}
