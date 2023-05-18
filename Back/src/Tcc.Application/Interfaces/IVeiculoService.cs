using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcc.Application.Interfaces
{
    public interface IVeiculoService
    {
        Task<VeiculoDto[]> SaveVeiculos(Guid associadoId, VeiculoDto[] models);
        Task<bool> DeleteVeiculo(Guid associadoId, Guid veiculoId);

        Task<VeiculoDto[]> GetVeiculosByAssociadoIdAsync(Guid associadoId);
        Task<VeiculoDto> GetVeiculoByIdsAsync(Guid associadoId, Guid veiculoId);
    }
}
