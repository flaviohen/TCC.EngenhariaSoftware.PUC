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
	public class A1UsuarioTest
	{
		public const string connectionString = @"Data Source=FLAVIO-NB\SQLEXPRESS;Initial Catalog=GestaoSaudePublica;Integrated Security=True";

		private readonly IA1UsuarioRepositorio _usuarioRepositorio;
		public A1UsuarioTest() 
		{
			var services = new ServiceCollection();
			services.AddTransient<IA1UsuarioRepositorio, A1UsuarioRepositorio>();
			services.AddScoped<IA1UsuarioRepositorio, A1UsuarioRepositorio>();
			services.AddEntityFrameworkSqlServer()
				.AddDbContext<GestaoSaudeContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("TCC.GestaoSaude.DataAccess")));
			var serviceProvider = services.BuildServiceProvider();

			_usuarioRepositorio = serviceProvider.GetService<IA1UsuarioRepositorio>();
		}

		[Fact]
		public void LogarTest()
		{
			A1Usuario usuario = new A1Usuario();
			usuario.A1UsuarioNome = "TESTE";
			usuario.A1UsuarioNumeroCpf = "36729578874";
			usuario.A1UsuarioSenha = "123456";

			var usuarioRetornado = new A1UsuarioBusiness(_usuarioRepositorio).Logar(usuario);

			Assert.True(usuarioRetornado != null);
		}
	}
}
