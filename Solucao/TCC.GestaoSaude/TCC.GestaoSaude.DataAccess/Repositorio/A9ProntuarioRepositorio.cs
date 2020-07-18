using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;
using TCC.GestaoSaude.DataAccess.Contexto;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class A9ProntuarioRepositorio : Repositorio<A9Prontuario>, IA9ProntuarioRepositorio
	{
		public A9ProntuarioRepositorio(GestaoSaudeContext context) : base(context) { }
	}
}
