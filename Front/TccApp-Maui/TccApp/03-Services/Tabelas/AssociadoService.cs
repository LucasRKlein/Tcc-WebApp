using TccApp.Domain.Interfaces;
using TccApp.Models;

namespace TccApp.Services
{
    public class AssociadoService : Repository<AssociadoModel>, IAssociadoService
    {
        //private readonly IAssociadoAcessorioService visAcessorioService;
        public AssociadoService(
            //IAssociadoAcessorioService visAcessorioService,
            )
        {
            //this.visAcessorioService = visAcessorioService;
        }

        /// <summary>
        /// Delete a associado e suas tabelas filhas, assim apaga imagens relacionadas.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteAssociado(AssociadoModel model)
        {
            //var listaAcessorio = visAcessorioService.GetByAssociadoId(model.Id);
            //foreach (var item in listaAcessorio)
            //{
            //    visAcessorioService.Delete(item);
            //}

            //var listaImagem = visImagemService.GetByAssociadoId(model.Id);
            //foreach (var item in listaImagem)
            //{
            //    File.Delete(item.ImagemUrl);

            //    visImagemService.Delete(item);
            //}

            Delete(model);

            return true;
        }

        public List<string> ValidateToSave(AssociadoModel model)
        {
            var lista = new List<string>();

            //if (string.IsNullOrEmpty(model.Origem))
            //{
            //    lista.Add("Campo Origem não pode ser nulo.");
            //}

            //if (string.IsNullOrEmpty(model.Responsavel)
            //    || string.IsNullOrEmpty(model.ResponsavelFone)
            //    || string.IsNullOrEmpty(model.ResponsavelDoc))
            //{
            //    lista.Add("Informe os dados do Responsável do veículo.");
            //}

            //if (string.IsNullOrEmpty(model.Veiculo)
            //    || string.IsNullOrEmpty(model.Placa)
            //    || (model.Ano == 0))
            //{
            //    lista.Add("Informe os dados do Veículo.");
            //}

            return lista;
        }

        public List<string> ValidateToFinalize(AssociadoModel model)
        {
            var lista = new List<string>();

            ////Responsável
            //var modelAssResponsavel = visImagemService.Get(x => x.AssociadoId == model.Id && x.Tipo == AssociadoImagemType.AssCliente);
            //if (modelAssResponsavel is null)
            //{
            //    lista.Add("Assinatura do responsável do veículo.");
            //}

            ////Entrega
            //if (string.IsNullOrEmpty(model.ResponsavelEntrega) || string.IsNullOrEmpty(model.ResponsavelEntregaDoc))
            //{
            //    lista.Add("Informe os dados do Responsável na entrega.");
            //}
            //var modelAssEntrega = visImagemService.Get(x => x.AssociadoId == model.Id && x.Tipo == AssociadoImagemType.AssResponasavelEntrega);
            //if (modelAssEntrega is null)
            //{
            //    lista.Add("Assinatura do responsável na entrega.");
            //}

            ////Associadodor
            //var modelAssAssociadodor = visImagemService.Get(x => x.AssociadoId == model.Id && x.Tipo == AssociadoImagemType.AssAssociadodor);
            //if (modelAssAssociadodor is null)
            //{
            //    lista.Add("Assinatura do associadodor.");
            //}

            //if (model.KmGuinchoEntrega <= model.KmGuinchoSaida)
            //{
            //    lista.Add("KM de entrega deve ser maior que KM de saída.");
            //}

            return lista;
        }
    }
}
