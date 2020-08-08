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
	public class A29AtendimentoTest
	{
		
		private readonly IA29AtendimentoRepositorio _atendimentoRepositorio;
		private readonly IA1UsuarioRepositorio _usuarioRepositorio;
		private readonly IA9ProntuarioRepositorio _prontuarioRepositorio;
		private readonly IA10RegistroEvolucaoEnfermagemRepositorio _registroEvolucaoEnfermagemRepositorio;
		private readonly IA13ProfissionalRepositorio _profissionalRepositorio;
		private readonly IA2UsuarioInternoRepositorio _usuarioInternoRepositorio;

		public A29AtendimentoTest() 
		{
			var services = new ServiceCollection();

			services.AddTransient<IA29AtendimentoRepositorio, A29AtendimentoRepositorio>();
			services.AddTransient<IA1UsuarioRepositorio, A1UsuarioRepositorio>();
			services.AddTransient<IA9ProntuarioRepositorio, A9ProntuarioRepositorio>();
			services.AddTransient<IA13ProfissionalRepositorio, A13ProfissionalRepositorio>();
			services.AddTransient<IA10RegistroEvolucaoEnfermagemRepositorio, A10RegistroEvolucaoEnfermagemRepositorio>();
			services.AddTransient<IA2UsuarioInternoRepositorio, A2UsuarioInternoRepositorio>();

			services.AddEntityFrameworkSqlServer()
				.AddDbContext<GestaoSaudeContext>(options => options.UseSqlServer(A1UsuarioTest.connectionString, b => b.MigrationsAssembly("TCC.GestaoSaude.DataAccess")));
			var serviceProvider = services.BuildServiceProvider();

			_atendimentoRepositorio = serviceProvider.GetService<IA29AtendimentoRepositorio>();
			_usuarioRepositorio = serviceProvider.GetService<IA1UsuarioRepositorio>();
			_prontuarioRepositorio = serviceProvider.GetService<IA9ProntuarioRepositorio>();
			_registroEvolucaoEnfermagemRepositorio = serviceProvider.GetService<IA10RegistroEvolucaoEnfermagemRepositorio>();
			_profissionalRepositorio = serviceProvider.GetService<IA13ProfissionalRepositorio>();
			_usuarioInternoRepositorio = serviceProvider.GetService<IA2UsuarioInternoRepositorio>();
		}

		[Fact]
		public void CadastrarAtendimentoTest()
		{
			List<string> includes = new List<string>();
			includes.Add("A3InformacaoCadastro");
			includes.Add("RelUsuarioPerfil");
			var usuario = _usuarioRepositorio.FindAll(c => c.RelUsuarioPerfil.FirstOrDefault(c => c.A1UsuarioId == 2).A6PerfilId == 4, includes).FirstOrDefault();
			A29Atendimento atendimento = new A29Atendimento();	
			atendimento.A29Data = DateTime.Now;
			atendimento.A3InformacaoCadastroId = usuario.A3InformacaoCadastro.ToList()[0].A3InformacaoCadastroId;
		
			var numeroAtendimento = new A29AtendimentoBusiness(_atendimentoRepositorio, null, null, null,_profissionalRepositorio,_usuarioInternoRepositorio).CadastrarAtendimento(atendimento);

			Assert.True(numeroAtendimento > 0);
		}

		[Fact]
		public void BuscarAtendimentoTest() 
		{
			int id = _atendimentoRepositorio.GetAll().FirstOrDefault().A29AtendimentoId;

			var atendimento = new A29AtendimentoBusiness(_atendimentoRepositorio, _prontuarioRepositorio, _registroEvolucaoEnfermagemRepositorio,_usuarioRepositorio,_profissionalRepositorio,_usuarioInternoRepositorio).BuscarAtendimento(id);

			Assert.True(atendimento != null);
		}



	}
}
