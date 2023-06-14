using System;
using System.Threading.Tasks;
using Tcc.Application.Dtos;

namespace Tcc.Application.Interfaces
{
    public interface IVistoriaImagemService
    {
        Task<VistoriaImagemDto> CreateVistoriaImagem(VistoriaImagemDto model);
        Task<VistoriaImagemDto> UpdateVistoriaImagem(Guid veiculoId, VistoriaImagemDto model);
        Task<bool> DeleteVistoriaImagem(Guid veiculoId);

        Task<VistoriaImagemDto[]> GetVistoriaImagensByVeiculoIdAsync(Guid veiculoId);
        Task<VistoriaImagemDto> GetByIdAsync(Guid imagemId);
    }
}
