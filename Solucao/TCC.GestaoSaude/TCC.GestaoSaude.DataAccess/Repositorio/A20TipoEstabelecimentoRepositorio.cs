using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class A20TipoEstabelecimentoRepositorio : Repositorio<A20TipoEstabelecimento>, IA20TipoEstabelecimentoRepositorio
	{
		public A20TipoEstabelecimentoRepositorio(GestaoSaudeContext context) : base(context) { }
	}
}
