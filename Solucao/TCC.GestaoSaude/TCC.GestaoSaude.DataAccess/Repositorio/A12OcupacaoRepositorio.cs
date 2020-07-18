using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;
using TCC.GestaoSaude.DataAccess.Contexto;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class A12OcupacaoRepositorio : Repositorio<A12Ocupacao>, IA12OcupacaoRepositorio
	{
		public A12OcupacaoRepositorio(GestaoSaudeContext context) : base(context){ }
	}
}
