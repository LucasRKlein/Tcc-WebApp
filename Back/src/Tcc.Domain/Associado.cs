﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tcc.Domain.Enum;
using Tcc.Domain.Identity;

namespace Tcc.Domain
{
    public class Associado
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string ImagemURL { get; set; }

        #region Dados de pessoa
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public SexoType Sexo { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        #endregion

        #region Dados de endereço
        public string RuaAvenida { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string EstadoNome { get; set; }
        public string CidadeNome { get; set; }
        #endregion
    }
}
