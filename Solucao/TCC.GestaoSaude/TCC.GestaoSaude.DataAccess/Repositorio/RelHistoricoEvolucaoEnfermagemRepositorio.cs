using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;
using TCC.GestaoSaude.DataAccess.Contexto;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class RelHistoricoEvolucaoEnfermagemRepositorio : Repositorio<RelHistoricoEvolucaoEnfermagem>, IRelHistoricoEvolucaoEnfermagemRepositorio
	{
		public RelHistoricoEvolucaoEnfermagemRepositorio(GestaoSaudeContext context) : base(context) { }
	}
}
