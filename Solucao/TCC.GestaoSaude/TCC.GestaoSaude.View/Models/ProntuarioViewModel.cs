using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TCC.GestaoSaude.View.Models
{
	public class ProntuarioViewModel
	{
		public string  RaciocinioMedico { get; set; }
		public string HipoteseDiagnostica { get; set; }
		public string CondutaTerapeuta { get; set; }
		public string PrescricaoMedica { get; set; }
		public string DescricaoCirurgica { get; set; }
		public string DiagnosticoMedico { get; set; }
		public List<RegistroEnfermagemViewModel> RegistrosEnfermagem { get; set; }

	}
}
