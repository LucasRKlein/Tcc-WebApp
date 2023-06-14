using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Tcc.Application.Interfaces;
using Tcc.Application;
using Tcc.Application.Services;
using Tcc.Application.Dtos;

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

        [HttpGet("GetVeiculoById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var associado = await _veiculoService.GetByIdAsync(id);
                if (associado == null) return NoContent();

                return Ok(associado);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar veiculo. Erro: {ex.Message}");
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

        [HttpPost]
        public async Task<IActionResult> Post(VeiculoDto model)
        {
            try
            {
                var veiculo = await _veiculoService.CreateVeiculo(model);
                if (veiculo == null) return NoContent();

                return Ok(veiculo);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar Veiculos. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, VeiculoDto model)
        {
            try
            {
                var veiculo = await _veiculoService.UpdateVeiculo(id, model);
                if (veiculo == null) return NoContent();

                return Ok(veiculo);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar associados. Erro: {ex.Message}");
            }
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(Guid id, VeiculoDto model)
        {
            try
            {
                var veiculo = await _veiculoService.UpdateVeiculo(id, model);
                if (veiculo == null) return NoContent();

                return Ok(veiculo);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar associados. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var veiculo = await _veiculoService.GetByIdAsync(id);
                if (veiculo == null) return NoContent();

                if (await _veiculoService.DeleteVeiculo(id))
                {
                    return Ok(new { message = "Deletado" });
                }
                else
                {
                    throw new Exception("Ocorreu um problem não específico ao tentar deletar Veiculo.");
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar associados. Erro: {ex.Message}");
            }
        }
    }
}
