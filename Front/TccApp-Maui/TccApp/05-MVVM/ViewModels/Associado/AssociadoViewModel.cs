using System;
using TccApp.Data;
using TccApp.Enum;
using TccApp.Models;

namespace TccApp.ViewModels
{
    [QueryProperty(nameof(ParamId), "id")]
    public partial class AssociadoViewModel : BaseItemViewModel<AssociadoModel>
    {
        #region Propriedades do viewmodel
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        DateTime dataInclusao;

        [ObservableProperty]
        string nome;

        [ObservableProperty]
        string imagemURL;

        [ObservableProperty]
        string cpf;

        [ObservableProperty]
        SexoType sexo;
        
        [ObservableProperty]
        DateTime? dataNascimento;
        
        [ObservableProperty]
        string celular;
        
        [ObservableProperty]
        string email;
        
        [ObservableProperty]
        string ruaAvenida;
        
        [ObservableProperty]
        string numero;
        
        [ObservableProperty]
        string complemento;
        
        [ObservableProperty]
        string bairro;
        
        [ObservableProperty]
        string cep;
        
        [ObservableProperty]
        string estadoNome;
        
        [ObservableProperty]
        string cidadeNome;

        // List<Veiculo> Veiculos;

        StatusCadastroType StatusCadastro;
        OrigemCadastroType OrigemCadastro;
        #endregion

        #region Utilizados em controles de tela
        [ObservableProperty]
        string sexoSelecionado;

        public List<string> ListaSexo { get; set; }
        #endregion

        public AssociadoViewModel(IRepository<AssociadoModel> repo) : base(repo)
        {
            Title = "Associado";
            //ActiveDelete = true;
            
            PrepareView();
        }

        private void PrepareView()
        {
            ListaSexo = new List<string>();
            PopulateListFromEnum<SexoType>(ListaSexo);
        }

        protected override void CreateNewModel()
        {
            base.CreateNewModel();
            Model.StatusCadastro = StatusCadastroType.PreCadastro;
            Model.OrigemCadastro = OrigemCadastroType.App;
            Model.Sexo = SexoType.NaoDefinido;
        }

        protected override void SetViewFromModel()
        {
            Id = Model.Id;
            DataInclusao = Model.DataInclusao;
            Nome = Model.Nome;

            ImagemURL = Model.ImagemURL;
            Cpf = Model.Cpf;
            Sexo = Model.Sexo;
            DataNascimento = Model.DataNascimento;

            Celular = Model.Celular;
            Email = Model.Email;
            RuaAvenida = Model.RuaAvenida;
            Numero = Model.Numero;
            Complemento = Model.Complemento;
            Bairro = Model.Bairro;
            Cep = Model.Cep;
            EstadoNome = Model.EstadoNome;
            CidadeNome = Model.CidadeNome;
            
            SetObjectsViewControls();
        }


        /// <summary>
        /// Setar objetos de controle de tela.
        /// </summary>
        private void SetObjectsViewControls()
        {
            SexoSelecionado = Model.Sexo.ToString();
        }

        protected override void SetModelFromView()
        {
            Model.Id = Id;
            Model.DataInclusao = DataInclusao;

            Model.Nome = Nome;
            Model.ImagemURL = ImagemURL;
            Model.Cpf = Cpf;
            Model.Sexo = (SexoType)System.Enum.Parse(typeof(SexoType), SexoSelecionado);

            Model.DataNascimento = DataNascimento;

            Model.Celular = Celular;
            Model.Email = Email;
            Model.RuaAvenida = RuaAvenida;
            Model.Numero = Numero;
            Model.Complemento = Complemento;
            Model.Bairro = Bairro;
            Model.Cep = Cep;
            Model.EstadoNome = EstadoNome;
            Model.CidadeNome = CidadeNome;
        }        

        protected override bool ValidateToSave()
        {
            if (string.IsNullOrEmpty(Model.Nome))
            {
                ValidateErrors.Add("Campo Nome não pode ser nulo.");
            }

            if (string.IsNullOrEmpty(Model.Celular))
            {
                ValidateErrors.Add("Informe os dados de contato do associado.");
            }

            if (ValidateErrors.Count > 0)
            {
                return false;
            }

            return true;
        }
    }
}
