﻿using Tcc.Application.Dtos;

namespace Tcc.Application
{
    public class VeiculoDto
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string MarcaModelo { get; set; }
        public string AnoModelo { get; set; }
        public string ValorFipe { get; set; }

        public int AssociadoId { get; set; }
        public AssociadoDto AssociadoDto { get; set; }
    }
}