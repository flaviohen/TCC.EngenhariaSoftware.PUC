using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;
using TCC.GestaoSaude.DataAccess.Contexto;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class A4CartaoVacinacaoRepositorio : Repositorio<A4CartaoVacinacao>, IA4CartaoVacinacaoRepositorio
	{
		public A4CartaoVacinacaoRepositorio(GestaoSaudeContext context) : base(context) { }
	}
}
