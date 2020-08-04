using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.View.Models
{
	public class UsuarioExternoViewModel
	{
        public string UsuarioID { get; set; }
        public string NumeroCPF { get; set; }
        public string NomeCompleto { get; set; }
        public string DataNascimento { get; set; }
		public string NomeMae { get; set; }
		public string NomePai { get; set; }
		public string NumeroCarteiraSUS { get; set; }
		public string TelefoneResidencial { get; set; }
		public string TelefoneCelular { get; set; }
		public string Email { get; set; }
	}
}
