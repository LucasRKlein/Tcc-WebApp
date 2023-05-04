using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tcc.Application.Interfaces;
using Tcc.Domain;
using Tcc.Persistence.Interface;

namespace Tcc.Application.Services
{
    public class VeiculoService : IVeiculoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IVeiculoPersist _veiculoPersist;
        private readonly IMapper _mapper;

        public VeiculoService(IGeralPersist geralPersist,
                           IVeiculoPersist veiculoPersist,
                           IMapper mapper)
        {
            _geralPersist = geralPersist;
            _veiculoPersist = veiculoPersist;
            _mapper = mapper;
        }

        public async Task AddVeiculo(int associadoId, VeiculoDto model)
        {
            try
            {
                var veiculo = _mapper.Map<Veiculo>(model);
                veiculo.AssociadoId = associadoId;

                _geralPersist.Add<Veiculo>(veiculo);

                await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<VeiculoDto[]> SaveVeiculos(int associadoId, VeiculoDto[] models)
        {
            try
            {
                var veiculos = await _veiculoPersist.GetVeiculosByAssociadoIdAsync(associadoId);
                if (veiculos == null) return null;

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddVeiculo(associadoId, model);
                    }
                    else
                    {
                        var veiculo = veiculos.FirstOrDefault(veiculo => veiculo.Id == model.Id);
                        model.AssociadoId = associadoId;

                        _mapper.Map(model, veiculo);

                        _geralPersist.Update<Veiculo>(veiculo);

                        await _geralPersist.SaveChangesAsync();
                    }
                }

                var veiculoRetorno = await _veiculoPersist.GetVeiculosByAssociadoIdAsync(associadoId);

                return _mapper.Map<VeiculoDto[]>(veiculoRetorno);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteVeiculo(int associadoId, int veiculoId)
        {
            try
            {
                var veiculo = await _veiculoPersist.GetVeiculoByIdsAsync(associadoId, veiculoId);
                if (veiculo == null) throw new Exception("Veiculo para delete não encontrado.");

                _geralPersist.Delete<Veiculo>(veiculo);
                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<VeiculoDto[]> GetVeiculosByAssociadoIdAsync(int associadoId)
        {
            try
            {
                var veiculos = await _veiculoPersist.GetVeiculosByAssociadoIdAsync(associadoId);
                if (veiculos == null) return null;

                var resultado = _mapper.Map<VeiculoDto[]>(veiculos);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<VeiculoDto> GetVeiculoByIdsAsync(int associadoId, int veiculoId)
        {
            try
            {
                var veiculo = await _veiculoPersist.GetVeiculoByIdsAsync(associadoId, veiculoId);
                if (veiculo == null) return null;

                var resultado = _mapper.Map<VeiculoDto>(veiculo);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
