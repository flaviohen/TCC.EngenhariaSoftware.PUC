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
using Microsoft.AspNetCore.Mvc.Formatters.Internal;

namespace TCC.GestaoSaude.Test
{
	public class A2UsuarioInternoTest
	{
		private readonly IA2UsuarioInternoRepositorio _usuarioInternoRepositorio;
		private readonly IA6PerfilRepositorio _perfilRepositorio;
		private readonly IA13ProfissionalRepositorio _profissionalRepositorio;

		public A2UsuarioInternoTest() 
		{
			var services = new ServiceCollection();
			services.AddTransient<IA2UsuarioInternoRepositorio, A2UsuarioInternoRepositorio>();
			services.AddTransient<IA6PerfilRepositorio, A6PerfilRepositorio>();
			services.AddTransient<IA13ProfissionalRepositorio, A13ProfissionalRepositorio>();

			services.AddEntityFrameworkSqlServer()
				.AddDbContext<GestaoSaudeContext>(options => options.UseSqlServer(A1UsuarioTest.connectionString, b => b.MigrationsAssembly("TCC.GestaoSaude.DataAccess")));
			var serviceProvider = services.BuildServiceProvider();

			_usuarioInternoRepositorio = serviceProvider.GetService<IA2UsuarioInternoRepositorio>();
			_perfilRepositorio = serviceProvider.GetService<IA6PerfilRepositorio>();
			_profissionalRepositorio = serviceProvider.GetService<IA13ProfissionalRepositorio>();

		}

		[Fact]
		public void LogarInternoTest()
		{
			A2UsuarioInterno usuario = new A2UsuarioInterno();
			usuario.A2UsuarioInternoNome = "TESTE Interno";
			usuario.A2UsuarioInternoEmail = "profissionalSaude@teste.com.br";
			usuario.A2UsuarioInternoSenha = "123456";

			var usuarioRetornado = new A1UsuarioBusiness(null, _usuarioInternoRepositorio, _perfilRepositorio, _profissionalRepositorio).LogarInterno(usuario);

			Assert.True(usuarioRetornado && usuario.Mensagens.Count == 0);
		}

		[Fact]
		public void CriarLoginInternoTest() 
		{

			A2UsuarioInterno usuario = new A2UsuarioInterno();
			usuario.A2UsuarioInternoNome = "Administrador";
			usuario.A2UsuarioInternoEmail = "administrador@teste.com.br";
			usuario.A2UsuarioInternoSenha = "123456";

			RelUsuarioInternoPerfil perfil = new RelUsuarioInternoPerfil();
			perfil.A6PerfilId = 1;
			usuario.RelUsuarioInternoPerfil = new List<RelUsuarioInternoPerfil>();
			usuario.RelUsuarioInternoPerfil.Add(perfil);

			//RelUsuarioInternoProfissional profissional = new RelUsuarioInternoProfissional();
			//profissional.A13ProfissionalCodigoCns = "700204985949127";
			//usuario.RelUsuarioInternoProfissional = new List<RelUsuarioInternoProfissional>();
			//usuario.RelUsuarioInternoProfissional.Add(profissional);

			var retorno = new A2UsuarioInternoBusiness(_usuarioInternoRepositorio).CriarLoginInterno(usuario);

			Assert.True(retorno && usuario.Mensagens.Count == 0);
		}

		[Fact]
		public void BuscarUsuarioInternoPorEmailTest() 
		{
			string email = _usuarioInternoRepositorio.GetAll().FirstOrDefault().A2UsuarioInternoEmail;

			var usuarioInterno = new A2UsuarioInternoBusiness(_usuarioInternoRepositorio).BuscarUsuarioInternoPorEmail(email);

			Assert.True(usuarioInterno != null);
		}

		[Fact]
		public void BuscarUsuarioInternoPorIDTest() 
		{
			var idUsuarioInterno = _usuarioInternoRepositorio.GetAll().FirstOrDefault().A2UsuarioInternoId;

			var usuarioInterno = new A2UsuarioInternoBusiness(_usuarioInternoRepositorio).BuscarUsuarioInternoPorID(idUsuarioInterno);

			Assert.True(usuarioInterno != null);
		}
	}
}
