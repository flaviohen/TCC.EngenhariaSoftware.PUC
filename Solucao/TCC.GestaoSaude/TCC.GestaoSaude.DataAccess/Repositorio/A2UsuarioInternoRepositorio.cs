using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class A2UsuarioInternoRepositorio : Repositorio<A2UsuarioInterno>, IA2UsuarioInternoRepositorio
	{
		public A2UsuarioInternoRepositorio(GestaoSaudeContext context) : base(context) { }
	}
}
