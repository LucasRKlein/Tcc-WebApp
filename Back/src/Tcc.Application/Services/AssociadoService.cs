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
        public async Task<AssociadoDto> AddAssociados(int userId, AssociadoDto model)
        {
            try
            {
                var Associado = _mapper.Map<Associado>(model);
                Associado.UserId = userId;

                _associadoPersist.Add<Associado>(Associado);

                if (await _associadoPersist.SaveChangesAsync())
                {
                    var AssociadoRetorno = await _associadoPersist.GetAssociadoByUserIdAsync(userId);

                    return _mapper.Map<AssociadoDto>(AssociadoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AssociadoDto> UpdateAssociado(int userId, AssociadoDto model)
        {
            try
            {
                var Associado = await _associadoPersist.GetAssociadoByUserIdAsync(userId);
                if (Associado == null) return null;

                model.Id = Associado.Id;
                model.UserId = userId;

                _mapper.Map(model, Associado);

                _associadoPersist.Update<Associado>(Associado);

                if (await _associadoPersist.SaveChangesAsync())
                {
                    var AssociadoRetorno = await _associadoPersist.GetAssociadoByUserIdAsync(userId);

                    return _mapper.Map<AssociadoDto>(AssociadoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAssociado(int userId)
        {
            try
            {
                var associado = await _associadoPersist.GetAssociadoByUserIdAsync(userId);
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

        public async Task<AssociadoDto> GetAssociadoByUserIdAsync(int userId)
        {
            try
            {
                var Associado = await _associadoPersist.GetAssociadoByUserIdAsync(userId);
                if (Associado == null) return null;

                var resultado = _mapper.Map<AssociadoDto>(Associado);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<AssociadoDto> GetAssociadoByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}