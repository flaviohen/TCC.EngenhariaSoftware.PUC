using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;
using TCC.GestaoSaude.DataAccess.Contexto;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class A23TipoAgendaRepositorio : Repositorio<A23TipoAgenda>, IA23TipoAgendaRepositorio
	{
		public A23TipoAgendaRepositorio(GestaoSaudeContext context) : base(context) { }
	}
}
