using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class RelEstabelecimentoLeitoRepositorio : Repositorio<RelEstabelecimentoLeito>, IRelEstabelecimentoLeitoRepositorio
	{
		public RelEstabelecimentoLeitoRepositorio(GestaoSaudeContext context) : base(context) { }
	}
}
