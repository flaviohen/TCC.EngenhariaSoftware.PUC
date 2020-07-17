using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class A10RegistroEvolucaoEnfermagemRepositorio : Repositorio<A10RegistroEvolucaoEnfermagem>, IA10RegistroEvolucaoEnfermagemRepositorio
	{
		public A10RegistroEvolucaoEnfermagemRepositorio(GestaoSaudeContext context) : base(context) { }
	}
}
