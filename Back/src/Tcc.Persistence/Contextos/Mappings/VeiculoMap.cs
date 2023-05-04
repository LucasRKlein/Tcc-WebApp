using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tcc.Domain;

namespace Tcc.Persistence.Contextos.Mappings
{
    public class VeiculoMap : IEntityTypeConfiguration<Veiculo>
    {
        public virtual void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.ToTable("Veiculo");
        }
    }
}
