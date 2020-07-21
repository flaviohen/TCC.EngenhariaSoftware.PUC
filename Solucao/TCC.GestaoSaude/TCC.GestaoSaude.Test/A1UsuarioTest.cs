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
		private readonly IA2UsuarioInternoRepositorio _usuarioInternoRepositorio;
		private readonly IA6PerfilRepositorio _perfilRepositorio;
		private readonly IA13ProfissionalRepositorio _profissionalRepositorio;
		public A1UsuarioTest() 
		{
			var services = new ServiceCollection();
			services.AddTransient<IA1UsuarioRepositorio, A1UsuarioRepositorio>();
			services.AddTransient<IA2UsuarioInternoRepositorio, A2UsuarioInternoRepositorio>();
			services.AddTransient<IA6PerfilRepositorio, A6PerfilRepositorio>();
			services.AddTransient<IA13ProfissionalRepositorio, A13ProfissionalRepositorio>();
			services.AddScoped<IA1UsuarioRepositorio, A1UsuarioRepositorio>();
			services.AddEntityFrameworkSqlServer()
				.AddDbContext<GestaoSaudeContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("TCC.GestaoSaude.DataAccess")));
			var serviceProvider = services.BuildServiceProvider();

			_usuarioRepositorio = serviceProvider.GetService<IA1UsuarioRepositorio>();
			_usuarioInternoRepositorio = serviceProvider.GetService<IA2UsuarioInternoRepositorio>();
			_perfilRepositorio = serviceProvider.GetService<IA6PerfilRepositorio>();
			_profissionalRepositorio = serviceProvider.GetService<IA13ProfissionalRepositorio>();
		}

		[Fact]
		public void LogarTest()
		{
			A1Usuario usuario = new A1Usuario();
			usuario.A1UsuarioNome = "TESTE";
			usuario.A1UsuarioNumeroCpf = "12312312312";
			usuario.A1UsuarioSenha = "123456";

			var usuarioRetornado = new A1UsuarioBusiness(_usuarioRepositorio, _usuarioInternoRepositorio, _perfilRepositorio, _profissionalRepositorio).Logar(usuario);

			Assert.True(usuarioRetornado && usuario.Mensagens.Count == 0);
		}

		[Fact]
		public void CriarLogin() 
		{
			A1Usuario usuario = new A1Usuario();
			usuario.A1UsuarioNome = "TESTE";
			usuario.A1UsuarioNumeroCpf = "12312312312";
			usuario.A1UsuarioSenha = "123456";

			var retorno = new A1UsuarioBusiness(_usuarioRepositorio, _usuarioInternoRepositorio, _perfilRepositorio, _profissionalRepositorio).CriarLogin(usuario);

			Assert.True(retorno && usuario.Mensagens.Count == 0);
		}
	}
}
