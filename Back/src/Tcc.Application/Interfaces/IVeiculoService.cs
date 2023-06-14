using System;
using System.Threading.Tasks;
using Tcc.Application.Dtos;

namespace Tcc.Application.Interfaces
{
    public interface IVeiculoService
    {
        Task<VeiculoDto[]> SaveVeiculos(Guid associadoId, VeiculoDto[] models);

        Task<VeiculoDto> CreateVeiculo(VeiculoDto model);
        Task<VeiculoDto> UpdateVeiculo(Guid veiculoId, VeiculoDto model);
        Task<bool> DeleteVeiculo(Guid veiculoId);

        Task<VeiculoDto[]> GetVeiculosByAssociadoIdAsync(Guid associadoId);
        Task<VeiculoDto> GetVeiculoByIdsAsync(Guid associadoId, Guid veiculoId);
        Task<VeiculoDto> GetByIdAsync(Guid veiculoId);
    }
}
