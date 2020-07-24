using System;
using System.Collections.Generic;
using System.Text;

namespace TCC.GestaoSaude.Models
{
	public class EmailModel
	{
		public string ParaEmail { get; set; }
		public string DoEmail { get; set; }
		public string Assunto { get; set; }
		public string CorpoEmail { get; set; }
		public List<string> EmailsCopia { get; set; }
		public List<string> EmailsCopiaOculta { get; set; }
	}
}
