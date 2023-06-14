
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Tcc.Application.Interfaces;
using Tcc.Application;
using Tcc.Application.Dtos;
using Tcc.Application.Services;
using Tcc.Api.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace Tcc.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VistoriaImagemController : ControllerBase
    {
        private readonly IVistoriaImagemService _vistoriaImagemService;
        private readonly IUtil _util;

        private readonly string _destino = "Vistorias";


        public VistoriaImagemController(IVistoriaImagemService VistoriaImagemService, IUtil util)
        {
            _vistoriaImagemService = VistoriaImagemService;
            _util = util;
        }

        [HttpGet("{veiculoId}")]
        public async Task<IActionResult> Get(Guid veiculoId)
        {
            try
            {
                var veiculos = await _vistoriaImagemService.GetVistoriaImagensByVeiculoIdAsync(veiculoId);
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
                var associado = await _vistoriaImagemService.GetByIdAsync(id);
                if (associado == null) return NoContent();

                return Ok(associado);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar veiculo. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(VistoriaImagemDto model)
        {
            try
            {
                var vistoriaImagem = await _vistoriaImagemService.CreateVistoriaImagem(model);
                if (vistoriaImagem == null) return NoContent();

                //if (vistoriaImagem.ImagemUrl != null || vistoriaImagem.ImagemUrl.Length > 0)
                //{
                //    await UploadImage(vistoriaImagem.Id);
                //}

                return Ok(vistoriaImagem);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar associados. Erro: {ex.Message}");
            }
        }

        [HttpPost("upload-image/{vistoriaImagemId}")]
        public async Task<IActionResult> UploadImage(Guid vistoriaImagemId)
        {
            try
            {
                var vistoriaImagem = await _vistoriaImagemService.GetByIdAsync(vistoriaImagemId);
                if (vistoriaImagem == null) return NoContent();

                if (Request.Form.Files[0] == null)
                {
                    return NoContent();
                }

                var file = Request.Form.Files[0];
                if (file != null && file.Length > 0)
                {
                    //_util.DeleteImage(vistoriaImagem.ImagemUrl, _destino);
                    vistoriaImagem.ImagemUrl = await _util.SaveImage(file, _destino);
                }
                var vistoriaImagemRetorno = await _vistoriaImagemService.UpdateVistoriaImagem(vistoriaImagemId, vistoriaImagem);

                return Ok(vistoriaImagemRetorno);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar imagem. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, VistoriaImagemDto model)
        {
            try
            {
                var vistoriaImagem = await _vistoriaImagemService.UpdateVistoriaImagem(id, model);
                if (vistoriaImagem == null) return NoContent();

                return Ok(vistoriaImagem);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar associados. Erro: {ex.Message}");
            }
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(Guid id, VistoriaImagemDto model)
        {
            try
            {
                var vistoriaImagem = await _vistoriaImagemService.UpdateVistoriaImagem(id, model);
                if (vistoriaImagem == null) return NoContent();

                return Ok(vistoriaImagem);
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
                var vistoriaImagem = await _vistoriaImagemService.GetByIdAsync(id);
                if (vistoriaImagem == null) return NoContent();

                if (await _vistoriaImagemService.DeleteVistoriaImagem(id))
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
