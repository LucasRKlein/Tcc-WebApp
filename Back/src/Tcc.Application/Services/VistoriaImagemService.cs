using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.Application.Dtos;
using Tcc.Application.Interfaces;
using Tcc.Domain;
using Tcc.Persistence.Interface;

namespace Tcc.Application.Services
{
    public class VistoriaImagemService : IVistoriaImagemService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IVistoriaImagemPersist _vistoriaImagemPersist;
        private readonly IMapper _mapper;

        public VistoriaImagemService(IGeralPersist geralPersist,
                           IVistoriaImagemPersist vistoriaImagemPersist,
                           IMapper mapper)
        {
            _geralPersist = geralPersist;
            _vistoriaImagemPersist = vistoriaImagemPersist;
            _mapper = mapper;
        }

        public async Task AddVistoriaImagem(Guid veiculoId, VistoriaImagemDto model)
        {
            try
            {
                var vistoriaImagem = _mapper.Map<VistoriaImagem>(model);
                vistoriaImagem.VeiculoId = veiculoId;

                _geralPersist.Add<VistoriaImagem>(vistoriaImagem);

                await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<VistoriaImagemDto> CreateVistoriaImagem(VistoriaImagemDto model)
        {
            try
            {
                var vistoriaImagem = _mapper.Map<VistoriaImagem>(model);

                _geralPersist.Add<VistoriaImagem>(vistoriaImagem);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var vistoriaImagemRetorno = await _vistoriaImagemPersist.GetByIdAsync(vistoriaImagem.Id);

                    return _mapper.Map<VistoriaImagemDto>(vistoriaImagemRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<VistoriaImagemDto> UpdateVistoriaImagem(Guid vistoriaImagemId, VistoriaImagemDto model)
        {
            try
            {
                var vistoriaImagem = await _vistoriaImagemPersist.GetByIdAsync(vistoriaImagemId);
                if (vistoriaImagem == null) return null;
                model.Id = vistoriaImagem.Id;

                _mapper.Map(model, vistoriaImagem);

                _geralPersist.Update<VistoriaImagem>(vistoriaImagem);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var vistoriaImagemRetorno = await _vistoriaImagemPersist.GetByIdAsync(vistoriaImagem.Id);

                    return _mapper.Map<VistoriaImagemDto>(vistoriaImagemRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteVistoriaImagem(Guid vistoriaImagemId)
        {
            try
            {
                var vistoriaImagem = await _vistoriaImagemPersist.GetByIdAsync(vistoriaImagemId);
                if (vistoriaImagem == null) throw new Exception("VistoriaImagem para delete não encontrado.");

                _geralPersist.Delete<VistoriaImagem>(vistoriaImagem);
                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<VistoriaImagemDto[]> GetVistoriaImagensByVeiculoIdAsync(Guid veiculoId)
        {
            try
            {
                var vistoriaImagems = await _vistoriaImagemPersist.GetVistoriaImagensByVeiculoIdAsync(veiculoId);
                if (vistoriaImagems == null) return null;

                var resultado = _mapper.Map<VistoriaImagemDto[]>(vistoriaImagems);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<VistoriaImagemDto> GetByIdAsync(Guid vistoriaImagemId)
        {
            try
            {
                var vistoriaImagem = await _vistoriaImagemPersist.GetByIdAsync(vistoriaImagemId);
                if (vistoriaImagem == null) return null;

                var resultado = _mapper.Map<VistoriaImagemDto>(vistoriaImagem);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
