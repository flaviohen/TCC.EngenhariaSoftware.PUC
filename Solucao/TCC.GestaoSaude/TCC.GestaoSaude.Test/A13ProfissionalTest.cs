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
	public class A13ProfissionalTest
	{
		
		private readonly IA13ProfissionalRepositorio _profissionalRepositorio;
		public A13ProfissionalTest() 
		{
			var services = new ServiceCollection();

			services.AddTransient<IA13ProfissionalRepositorio, A13ProfissionalRepositorio>();

			services.AddEntityFrameworkSqlServer()
				.AddDbContext<GestaoSaudeContext>(options => options.UseSqlServer(A1UsuarioTest.connectionString, b => b.MigrationsAssembly("TCC.GestaoSaude.DataAccess")));
			var serviceProvider = services.BuildServiceProvider();

			_profissionalRepositorio = serviceProvider.GetService<IA13ProfissionalRepositorio>();
		}

		[Fact]
		public void CadastrarProfissionalTest()
		{
			A13Profissional profissional = new A13Profissional();
			profissional.A13ProfissionalCodigoCns = "700204985949127";
			profissional.A13ProfissionalCodigoSus = "3FA1DA58CFEF8104";
			profissional.A13ProfissionalNome = "ROMARIO DOS SANTOS OLIVEIRA";
			profissional.A13ProfissionalData = DateTime.Now;
		

			var usuarioRetornado = new A13ProfissionalBusiness(_profissionalRepositorio).CadastrarProfissional(profissional);

			Assert.True(usuarioRetornado != null && profissional.Mensagens.Count == 0);
		}

		
	}
}
