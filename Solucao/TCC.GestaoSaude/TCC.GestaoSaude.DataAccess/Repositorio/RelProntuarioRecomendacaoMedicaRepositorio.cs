using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class RelProntuarioRecomendacaoMedicaRepositorio : Repositorio<RelProntuarioRecomendacaoMedica>, IRelProntuarioRecomendacaoMedicaRepositorio
	{
		public RelProntuarioRecomendacaoMedicaRepositorio(GestaoSaudeContext context) : base(context){ }
	}
}
