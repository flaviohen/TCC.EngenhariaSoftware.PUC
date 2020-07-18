using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;
using TCC.GestaoSaude.DataAccess.Contexto;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class A24RecomendacaoMedicaRepositorio : Repositorio<A24RecomendacaoMedica>, IA24RecomendacaoMedicaRepositorio
	{
		public A24RecomendacaoMedicaRepositorio(GestaoSaudeContext context) : base(context) { }
	}
}
