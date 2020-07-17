using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class A26LeitoRepositorio : Repositorio<A26Leito>,IA26LeitoRepositorio
	{
		public A26LeitoRepositorio(GestaoSaudeContext context) : base(context) { }
	}
}
