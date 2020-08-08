using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCC.GestaoSaude.View.Models
{
	public class RegistroEnfermagemViewModel
	{
		public int ID { get; set; }
		public string Data { get; set; }
		public string Hora { get; set; }
		public string Descricao { get; set; }
		public string Profissional { get; set; }
		public bool EhRegistroNovo { get; set; }
	}
}
