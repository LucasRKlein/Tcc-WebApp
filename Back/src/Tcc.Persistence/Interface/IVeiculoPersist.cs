using System;
using System.Threading.Tasks;
using Tcc.Domain;

namespace Tcc.Persistence.Interface
{
    public interface IVeiculoPersist
    {
        /// <summary>
        /// Método get que retornará uma lista de veiculos por associadoId. 
        /// </summary>
        /// <param name="associadoId">Código chave da tabela Associado</param>
        /// <returns>Array de Veiculos</returns>
        Task<Veiculo[]> GetVeiculosByAssociadoIdAsync(Guid associadoId);

        /// <summary>
        /// Método get que retornará apenas 1 Veiculo, utilizando o id e o associadoId, ficou obsoleto pois agora todos os IDs, são GUID
        /// </summary>
        /// <param name="associadoId">Código chave da tabela Associado</param>
        /// <param name="id">Código chave da tabela Veiculo</param>
        /// <returns>Apenas 1 veiculo</returns>
        Task<Veiculo> GetVeiculoByIdsAsync(Guid associadoId, Guid id);

        /// <summary>
        /// Novo metodo para retornar 1 veiculo pelo ID, agora que todos os IDS são GUID, não é mais necessario o ID do Associado
        /// </summary>
        /// <param name="veiculoId"></param>
        /// <returns></returns>
        Task<Veiculo> GetByIdAsync(Guid veiculoId);
    }
}
