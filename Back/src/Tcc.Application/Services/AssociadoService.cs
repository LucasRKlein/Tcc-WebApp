using System;
using System.Threading.Tasks;
using AutoMapper;
using Tcc.Application.Interface;
using Tcc.Application.Dtos;
using Tcc.Domain;
using Tcc.Persistence.Interface;
using Tcc.Persistence.Models;
using Tcc.Application.Interfaces;
using Tcc.Persistence;

namespace Tcc.Application.Services
{
    public class AssociadoService : IAssociadoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IAssociadoPersist _associadoPersist;
        private readonly IMapper _mapper;
        public AssociadoService(IGeralPersist geralPersist,
                             IAssociadoPersist associadoPersist,
                             IMapper mapper)
        {
            _geralPersist = geralPersist;
            _associadoPersist = associadoPersist;
            _mapper = mapper;
        }

        public async Task<AssociadoDto> CreateAssociado(AssociadoDto model)
        {
            try
            {
                var associado = _mapper.Map<Associado>(model);

                _geralPersist.Add<Associado>(associado);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var associadoRetorno = await _associadoPersist.GetAssociadoByIdAsync(associado.Id);

                    return _mapper.Map<AssociadoDto>(associadoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AssociadoDto> UpdateAssociado(Guid associadoId, AssociadoDto model)
        {
            try
            {
                var associado = await _associadoPersist.GetAssociadoByIdAsync(associadoId);
                if (associado == null) return null;
                model.Id = associado.Id;

                _mapper.Map(model, associado);

                _geralPersist.Update<Associado>(associado);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var associadoRetorno = await _associadoPersist.GetAssociadoByIdAsync(associado.Id);

                    return _mapper.Map<AssociadoDto>(associadoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAssociado(Guid associadoId)
        {
            try
            {
                var associado = await _associadoPersist.GetAssociadoByIdAsync(associadoId);
                if (associado == null) throw new Exception("Associado para delete não encontrado.");

                _geralPersist.Delete<Associado>(associado);
                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<AssociadoDto>> GetAllAssociadosAsync(PageParams pageParams)
        {
            try
            {
                var Associados = await _associadoPersist.GetAllAssociadosAsync(pageParams);
                if (Associados == null) return null;

                var resultado = _mapper.Map<PageList<AssociadoDto>>(Associados);

                resultado.CurrentPage = Associados.CurrentPage;
                resultado.TotalPages = Associados.TotalPages;
                resultado.PageSize = Associados.PageSize;
                resultado.TotalCount = Associados.TotalCount;

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AssociadoDto> GetAssociadoByIdAsync(Guid associadoId)
        {
            try
            {
                var associado = await _associadoPersist.GetAssociadoByIdAsync(associadoId);
                if (associado == null) return null;

                var resultado = _mapper.Map<AssociadoDto>(associado);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}