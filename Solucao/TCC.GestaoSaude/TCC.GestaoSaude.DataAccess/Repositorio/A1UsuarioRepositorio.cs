using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class A1UsuarioRepositorio : Repositorio<A1Usuario>, IA1UsuarioRepositorio
	{
		public A1UsuarioRepositorio(GestaoSaudeContext context) : base(context) { }
	}
}
