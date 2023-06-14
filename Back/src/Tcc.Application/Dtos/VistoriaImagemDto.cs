using System;
using System.ComponentModel.DataAnnotations;
using Tcc.Application.Dtos.Base;
using Tcc.Domain;

namespace Tcc.Application.Dtos
{
    public class VistoriaImagemDto : BaseDto
    {
        public Guid VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }
        public string ImagemUrl { get; set; }
        public string ImagemBase64 { get; set; }
    }
}
