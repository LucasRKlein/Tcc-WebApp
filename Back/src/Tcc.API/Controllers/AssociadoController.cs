using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tcc.Api.Helpers;
using Tcc.API.Extensions;
using Tcc.Application.Dtos;
using Tcc.Application.Interface;
using Tcc.Application.Interfaces;
using Tcc.Application.Services;
using Tcc.Persistence.Models;

namespace Tcc.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AssociadoController : ControllerBase
    {
        private readonly IAssociadoService _associadoService;
        private readonly IUtil _util;
        private readonly IAccountService _accountService;

        private readonly string _destino = "Associados";

        public AssociadoController(IAssociadoService associadoService, IUtil util, IAccountService accountService)
        {
            _util = util;
            _associadoService = associadoService;
            _accountService = accountService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] PageParams pageParams)
        {
            try
            {
                var associados = await _associadoService.GetAllAssociadosAsync(pageParams);
                if (associados == null) return NoContent();

                Response.AddPagination(associados.CurrentPage,
                                       associados.PageSize,
                                       associados.TotalCount,
                                       associados.TotalPages);

                return Ok(associados);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar associados. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var associado = await _associadoService.GetAssociadoByIdAsync(id);
                if (associado == null) return NoContent();

                return Ok(associado);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar associado. Erro: {ex.Message}");
            }
        }

        [HttpPost("upload-image/{associadoId}")]
        public async Task<IActionResult> UploadImage(Guid associadoId)
        {
            try
            {
                var associado = await _associadoService.GetAssociadoByIdAsync(associadoId);
                if (associado == null) return NoContent();

                var file = Request.Form.Files[0];
                if(file.Length > 0)
                {
                    _util.DeleteImage(associado.ImagemURL, _destino);
                    associado.ImagemURL = await _util.SaveImage(file, _destino);
                }
                var associadoRetorno = await _associadoService.UpdateAssociado(associadoId, associado);

                return Ok(associadoRetorno);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar associados. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(AssociadoDto model)
        {
            try
            {
                var associado = await _associadoService.CreateAssociado(model);
                if (associado == null) return NoContent();

                return Ok(associado);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar associados. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, AssociadoDto model)
        {
            try
            {
                var associado = await _associadoService.UpdateAssociado(id, model);
                if (associado == null) return NoContent();

                return Ok(associado);
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
                var associado = await _associadoService.GetAssociadoByIdAsync(id);
                if (associado == null) return NoContent();

                if (await _associadoService.DeleteAssociado(id))
                {
                    _util.DeleteImage(associado.ImagemURL, _destino);
                    return Ok(new { message = "Deletado" });
                }
                else
                {
                    throw new Exception("Ocorreu um problem não específico ao tentar deletar Associado.");
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
