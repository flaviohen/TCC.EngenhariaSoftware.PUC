using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;
using TCC.GestaoSaude.DataAccess.Contexto;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class A6PerfilRepositorio : Repositorio<A6Perfil>, IA6PerfilRepositorio
	{
		public A6PerfilRepositorio(GestaoSaudeContext context) : base(context) { }
	}
}
