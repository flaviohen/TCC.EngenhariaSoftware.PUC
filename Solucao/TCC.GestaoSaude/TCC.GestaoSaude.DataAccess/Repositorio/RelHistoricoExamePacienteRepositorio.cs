using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;
using TCC.GestaoSaude.DataAccess.Contexto;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class RelHistoricoExamePacienteRepositorio : Repositorio<RelHistoricoExamePaciente>, IRelHistoricoExamePacienteRepositorio
	{
		public RelHistoricoExamePacienteRepositorio(GestaoSaudeContext context) : base(context) { }
	}
}
