using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.Domain;

namespace Tcc.Persistence.Contextos
{
    public class AssociadoMap : IEntityTypeConfiguration<Associado>
    {
        public virtual void Configure(EntityTypeBuilder<Associado> builder)
        {
            builder.ToTable("Associado");
        }
    }
}
