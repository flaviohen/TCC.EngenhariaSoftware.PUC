using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;
using TCC.GestaoSaude.DataAccess.Contexto;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class RelUsuarioInternoPerfilRepositorio : Repositorio<RelUsuarioInternoPerfil>, IRelUsuarioInternoPerfilRepositorio
	{
		public RelUsuarioInternoPerfilRepositorio(GestaoSaudeContext context) : base(context) { }
	}
}
