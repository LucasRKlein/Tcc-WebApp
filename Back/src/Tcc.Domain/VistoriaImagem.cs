using System;
using System.ComponentModel.DataAnnotations;
using Tcc.Domain.Base;

namespace Tcc.Domain
{
    public class VistoriaImagem : BaseModel
    {
        public Guid VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }
        public string ImagemUrl { get; set; }
        public string ImagemBase64 { get; set; }
    }
}
