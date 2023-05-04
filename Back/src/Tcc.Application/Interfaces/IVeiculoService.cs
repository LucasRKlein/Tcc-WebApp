using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcc.Application.Interfaces
{
    public interface IVeiculoService
    {
        Task<VeiculoDto[]> SaveVeiculos(int associadoId, VeiculoDto[] models);
        Task<bool> DeleteVeiculo(int associadoId, int loteId);

        Task<VeiculoDto[]> GetVeiculosByAssociadoIdAsync(int associadoId);
        Task<VeiculoDto> GetVeiculoByIdsAsync(int associadoId, int loteId);
    }
}
