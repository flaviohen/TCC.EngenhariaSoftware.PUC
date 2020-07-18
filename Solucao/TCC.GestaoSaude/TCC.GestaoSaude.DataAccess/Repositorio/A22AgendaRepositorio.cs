using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;
using TCC.GestaoSaude.DataAccess.Contexto;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class A22AgendaRepositorio : Repositorio<A22Agenda>,IA22AgendaRepositorio
	{
		public A22AgendaRepositorio(GestaoSaudeContext context) : base(context) { }
	}
}
