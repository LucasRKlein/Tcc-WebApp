using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.Domain;

namespace Tcc.Persistence.Mappings
{
    public class VistoriaImagemMap : IEntityTypeConfiguration<VistoriaImagem>
    {
        public virtual void Configure(EntityTypeBuilder<VistoriaImagem> builder)
        {
            builder.ToTable("VistoriaImagem");

            builder.HasKey(x => x.Id);
            builder.Property(f => f.DataInclusao).IsRequired();
            builder.Property(f => f.DataAlteracao).IsRequired(false);
            builder.Property(f => f.RegistroAtivo).IsRequired();
        }
    }
}
