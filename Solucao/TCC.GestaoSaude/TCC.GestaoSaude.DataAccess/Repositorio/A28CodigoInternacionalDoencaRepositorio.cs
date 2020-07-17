using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class A28CodigoInternacionalDoencaRepositorio : Repositorio<A28CodigoInternacionalDoenca>,IA28CodigoInternacionalDoencaRepositorio
	{
		public A28CodigoInternacionalDoencaRepositorio(GestaoSaudeContext context) : base(context) { }
	}
}
