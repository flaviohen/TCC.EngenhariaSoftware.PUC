using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;
using TCC.GestaoSaude.DataAccess.Contexto;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class A3InformacaoCadastroRepositorio : Repositorio<A3InformacaoCadastro>, IA3InformacaoCadastroRepositorio
	{
		public A3InformacaoCadastroRepositorio(GestaoSaudeContext context) : base(context) { }
	}
}
