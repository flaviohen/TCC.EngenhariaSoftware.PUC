using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class RelProfissionalOcupacaoRepositorio : Repositorio<RelProfissionalOcupacao>, IRelProfissionalOcupacaoRepositorio
	{
		public RelProfissionalOcupacaoRepositorio(GestaoSaudeContext context) : base(context){ }
	}
}
