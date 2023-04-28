using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Tcc.Api.Helpers;
using Tcc.Application.Dtos;
using Tcc.Application.Interface;
using Tcc.Domain.Identity;
using Tcc.Persistence.Models;
using Tcc.Application.Interfaces;
using Tcc.API.Extensions;

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

        private readonly string _destino = "Images";

        public AssociadoController(IAssociadoService associadoService,
                                 IUtil util,
                                 IAccountService accountService)
        {
            _util = util;
            _accountService = accountService;
            _associadoService = associadoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParams pageParams)
        {
            try
            {
                var associados = await _associadoService.GetAllAssociadosAsync(User.GetUserId(), pageParams);
                if (associados == null) return NoContent();

                Response.AddPagination(associados.CurrentPage, associados.PageSize, associados.TotalCount, associados.TotalPages);

                return Ok(associados);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar associados. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var associado = await _associadoService.GetAssociadoByIdAsync(User.GetUserId(), id);
                if (associado == null) return NoContent();

                return Ok(associado);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar associados. Erro: {ex.Message}");
            }
        }

        [HttpPost("upload-image/{associadoId}")]
        public async Task<IActionResult> UploadImage(int associadoId)
        {
            try
            {
                var associado = await _associadoService.GetAssociadoByIdAsync(User.GetUserId(), associadoId);
                if (associado == null) return NoContent();

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    _util.DeleteImage(associado.ImagemURL, _destino);
                    associado.ImagemURL = await _util.SaveImage(file, _destino);
                }
                var AssociadoRetorno = await _associadoService.UpdateAssociado(User.GetUserId(), associadoId, associado);

                return Ok(AssociadoRetorno);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar realizar upload de foto do associado. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(AssociadoDto model)
        {
            try
            {
                var associado = await _associadoService.AddAssociados(User.GetUserId(), model);
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
        public async Task<IActionResult> Put(int id, AssociadoDto model)
        {
            try
            {
                var associado = await _associadoService.UpdateAssociado(User.GetUserId(), id, model);
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var associado = await _associadoService.GetAssociadoByIdAsync(User.GetUserId(), id);
                if (associado == null) return NoContent();

                if (await _associadoService.DeleteAssociado(User.GetUserId(), id))
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
