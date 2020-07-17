using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class A21EstabelecimentoRepositorio : Repositorio<A21Estabelecimento>, IA21EstabelecimentoRepositorio
	{
		public A21EstabelecimentoRepositorio(GestaoSaudeContext context) : base(context) { }
	}
}
