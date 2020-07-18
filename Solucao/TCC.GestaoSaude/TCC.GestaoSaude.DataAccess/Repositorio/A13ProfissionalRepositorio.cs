using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;
using TCC.GestaoSaude.DataAccess.Contexto;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class A13ProfissionalRepositorio : Repositorio<A13Profissional>, IA13ProfissionalRepositorio
	{
		public A13ProfissionalRepositorio(GestaoSaudeContext context) : base(context) { }
	}
}
