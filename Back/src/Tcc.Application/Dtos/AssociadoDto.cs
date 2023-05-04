using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tcc.Domain.Enum;

namespace Tcc.Application.Dtos
{
    public class AssociadoDto
    {
        public int Id { get; set; }


        #region Dados de pessoa

        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "Não é uma imagem válida. (gif, jpg, jpeg, bmp ou png)")]
        public string ImagemURL { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(18, ErrorMessage = "O campo {0} precisa ter no máximo {1} caracteres")]
        public string Cpf { get; set; }

        [Range(0, 2, ErrorMessage = "O campo {0} está fora do intervalo permitido")]
        public SexoType Sexo { get; set; }
        public DateTime? DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Phone(ErrorMessage = "O campo {0} está com número inválido")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "e-mail")]
        [EmailAddress(ErrorMessage = "É necessário ser um {0} válido")]
        public string Email { get; set; }
        #endregion

        #region Dados de endereço
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter no máximo {1} caracteres")]
        public string RuaAvenida { get; set; }

        [StringLength(10, ErrorMessage = "O campo {0} precisa ter no máximo {1} caracteres")]
        public string Numero { get; set; }

        [StringLength(50, ErrorMessage = "O campo {0} precisa ter no máximo {1} caracteres")]
        public string Complemento { get; set; }

        [StringLength(50, ErrorMessage = "O campo {0} precisa ter no máximo {1} caracteres")]
        public string Bairro { get; set; }

        [StringLength(10, ErrorMessage = "O campo {0} precisa ter no máximo {1} caracteres")]
        public string Cep { get; set; }

        [StringLength(50, ErrorMessage = "O campo {0} precisa ter no máximo {1} caracteres")]
        public string EstadoNome { get; set; }

        [StringLength(50, ErrorMessage = "O campo {0} precisa ter no máximo {1} caracteres")]
        public string CidadeNome { get; set; }
        #endregion

        public IEnumerable<VeiculoDto> Veiculos { get; set; }
    }
}
