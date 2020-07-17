using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class A15CargaHorariaProfissionalRepositorio : Repositorio<A15CargaHorariaProfissional>, IA15CargaHorariaProfissionalRepositorio
	{
		public A15CargaHorariaProfissionalRepositorio(GestaoSaudeContext context) : base(context) { }
	}
}
