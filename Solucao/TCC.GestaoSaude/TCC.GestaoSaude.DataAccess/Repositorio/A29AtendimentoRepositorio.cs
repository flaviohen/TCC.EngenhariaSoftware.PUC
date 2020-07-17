using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class A29AtendimentoRepositorio : Repositorio<A29Atendimento>, IA29AtendimentoRepositorio
	{
		public A29AtendimentoRepositorio(GestaoSaudeContext context) : base(context) { }
	}
}
