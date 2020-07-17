using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class A5VacinaRepositorio : Repositorio<A5Vacina>, IA5VacinaRepositorio
	{
		public A5VacinaRepositorio(GestaoSaudeContext context) : base(context) { }
	}
}
