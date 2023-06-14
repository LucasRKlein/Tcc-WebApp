using System;
using System.Threading.Tasks;
using Tcc.Domain;

namespace Tcc.Persistence.Interface
{
    public interface IVistoriaImagemPersist
    {
        /// <summary>
        /// Método get que retornará uma lista de imagens por veiculoId. 
        /// </summary>
        /// <param name="veiculoId">Código chave da tabela Veiculo</param>
        /// <returns>Array de Veiculos</returns>
        Task<VistoriaImagem[]> GetVistoriaImagensByVeiculoIdAsync(Guid veiculoId);

        /// <summary>
        /// Retorna uma imagem pelo id
        /// </summary>
        /// <param name="imagemId"></param>
        /// <returns></returns>
        Task<VistoriaImagem> GetByIdAsync(Guid imagemId);
    }
}
