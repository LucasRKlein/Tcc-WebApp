using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Tcc.Application.Interfaces;
using Tcc.Application;

namespace Tcc.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoService _veiculoService;

        public VeiculoController(IVeiculoService VeiculoService)
        {
            _veiculoService = VeiculoService;
        }

        [HttpGet("{associadoId}")]
        public async Task<IActionResult> Get(Guid associadoId)
        {
            try
            {
                var veiculos = await _veiculoService.GetVeiculosByAssociadoIdAsync(associadoId);
                if (veiculos == null) return NoContent();

                return Ok(veiculos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar veiculos. Erro: {ex.Message}");
            }
        }

        [HttpPut("{associadoId}")]
        public async Task<IActionResult> SaveVeiculos(Guid associadoId, VeiculoDto[] models)
        {
            try
            {
                var veiculos = await _veiculoService.SaveVeiculos(associadoId, models);
                if (veiculos == null) return NoContent();

                return Ok(veiculos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar salvar veiculos. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{associadoId}/{veiculoId}")]
        public async Task<IActionResult> Delete(Guid associadoId, Guid veiculoId)
        {
            try
            {
                var veiculo = await _veiculoService.GetVeiculoByIdsAsync(associadoId, veiculoId);
                if (veiculo == null) return NoContent();

                return await _veiculoService.DeleteVeiculo(veiculo.AssociadoId, veiculo.Id)
                       ? Ok(new { message = "Veiculo Deletado" })
                       : throw new Exception("Ocorreu um problem não específico ao tentar deletar Veiculo.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar veiculos. Erro: {ex.Message}");
            }
        }
    }
}
