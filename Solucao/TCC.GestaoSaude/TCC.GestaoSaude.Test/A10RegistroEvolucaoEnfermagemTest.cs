using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using Xunit;
using Newtonsoft.Json.Bson;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.DataAccess.Repositorio;
using TCC.GestaoSaude.DataAccess.Contexto;
using Microsoft.EntityFrameworkCore;
using TCC.GestaoSaude.Business;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.Test
{
	public class A10RegistroEvolucaoEnfermagemTest
	{
		
		private readonly IA10RegistroEvolucaoEnfermagemRepositorio _registroEvolucaoEnfermagemRepositorio;
		public A10RegistroEvolucaoEnfermagemTest() 
		{
			var services = new ServiceCollection();

			services.AddTransient<IA10RegistroEvolucaoEnfermagemRepositorio, A10RegistroEvolucaoEnfermagemRepositorio>();

			services.AddEntityFrameworkSqlServer()
				.AddDbContext<GestaoSaudeContext>(options => options.UseSqlServer(A1UsuarioTest.connectionString, b => b.MigrationsAssembly("TCC.GestaoSaude.DataAccess")));
			var serviceProvider = services.BuildServiceProvider();

			_registroEvolucaoEnfermagemRepositorio = serviceProvider.GetService<IA10RegistroEvolucaoEnfermagemRepositorio>();
		}

		[Fact]
		public void CadastrarRegistrosEnfermagemTest()
		{
			
			A10RegistroEvolucaoEnfermagem registroEvolucaoEnfermagem = new A10RegistroEvolucaoEnfermagem();
			registroEvolucaoEnfermagem.A10RegistroEvolucaoEnfermagemData = DateTime.Now;
			registroEvolucaoEnfermagem.A10RegistroEvolucaoEnfermagemDescrição = "O Paciente apresenta febre alta";
			registroEvolucaoEnfermagem.A10RegistroEvolucaoEnfermagemHora = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
			registroEvolucaoEnfermagem.A13ProfissionalCodigoCns = "1";

			List<A10RegistroEvolucaoEnfermagem> lstRegistros = new List<A10RegistroEvolucaoEnfermagem>();
			lstRegistros.Add(registroEvolucaoEnfermagem);
			var registroCadastrados = new A10RegistroEvolucaoEnfermagemBusiness(_registroEvolucaoEnfermagemRepositorio).CadastrarRegistrosEnfermagem(lstRegistros);

			Assert.True(registroCadastrados.Count > 0);

			_registroEvolucaoEnfermagemRepositorio.Delete(registroEvolucaoEnfermagem);
		}

		[Fact]
		public void BuscarRegistroEvolucaoEnfermagemPorCodigoTest() 
		{
			var id = _registroEvolucaoEnfermagemRepositorio.GetAll().FirstOrDefault().A10RegistroEvolucaoEnfermagemId;

			var registroRetornado = new A10RegistroEvolucaoEnfermagemBusiness(_registroEvolucaoEnfermagemRepositorio).BuscarRegistroEvolucaoEnfermagemPorCodigo(id);

			Assert.True(registroRetornado != null);
		}


	}
}
