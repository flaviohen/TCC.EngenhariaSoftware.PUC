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
	public class A6PerfilTest
	{
		private readonly IA6PerfilRepositorio _perfilRepositorio;
		
		public A6PerfilTest() 
		{
			var services = new ServiceCollection();
			services.AddTransient<IA6PerfilRepositorio, A6PerfilRepositorio>();

			services.AddEntityFrameworkSqlServer()
				.AddDbContext<GestaoSaudeContext>(options => options.UseSqlServer(A1UsuarioTest.connectionString, b => b.MigrationsAssembly("TCC.GestaoSaude.DataAccess")));
			var serviceProvider = services.BuildServiceProvider();

			_perfilRepositorio = serviceProvider.GetService<IA6PerfilRepositorio>();
			
		}

		[Fact]
		public void CadastrarPerfilTest()
		{
			A6Perfil perfil = new A6Perfil();
			perfil.A6PerfilDescricao = "Paciente";
			
			var perfilCadastrado = new A6PerfilBusiness(_perfilRepositorio).CadastrarPerfil(perfil).Result;

			Assert.True(perfilCadastrado && perfil.Mensagens.Count == 0);
		}

		
	}
}
