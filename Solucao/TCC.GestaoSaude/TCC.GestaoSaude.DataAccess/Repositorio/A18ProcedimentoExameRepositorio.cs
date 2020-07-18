using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;
using TCC.GestaoSaude.DataAccess.Contexto;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class A18ProcedimentoExameRepositorio : Repositorio<A18ProcedimentoExame>, IA18ProcedimentoExameRepositorio
	{
		public A18ProcedimentoExameRepositorio(GestaoSaudeContext context) : base(context) { }
	}
}
