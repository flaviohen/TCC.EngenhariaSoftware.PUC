using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;
using TCC.GestaoSaude.DataAccess.Contexto;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class A11RegistroEvolucaoMedicaDiariaRepositorio : Repositorio<A11RegistroEvolucaoMedicaDiaria>, IA11RegistroEvolucaoMedicaDiariaRepositorio
	{
		public A11RegistroEvolucaoMedicaDiariaRepositorio(GestaoSaudeContext context) : base(context) { }
	}
}
