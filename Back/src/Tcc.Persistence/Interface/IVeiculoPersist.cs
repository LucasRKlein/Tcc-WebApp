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
        Task<Veiculo[]> GetVeiculosByAssociadoIdAsync(int associadoId);

        /// <summary>
        /// Método get que retornará apenas 1 Veiculo
        /// </summary>
        /// <param name="associadoId">Código chave da tabela Associado</param>
        /// <param name="id">Código chave da tabela Veiculo</param>
        /// <returns>Apenas 1 veiculo</returns>
        Task<Veiculo> GetVeiculoByIdsAsync(int associadoId, int id);
    }
}
