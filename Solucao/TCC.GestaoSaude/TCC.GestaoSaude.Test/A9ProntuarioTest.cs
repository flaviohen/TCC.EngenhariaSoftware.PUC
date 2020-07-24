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
	public class A9ProntuarioTest
	{
		private readonly IA9ProntuarioRepositorio _prontuarioRepositorio;
		private readonly IA29AtendimentoRepositorio _atendimentoRepositorio;
		private readonly IA10RegistroEvolucaoEnfermagemRepositorio _registroEvolucaoEnfermagemRepositorio;
		private readonly IRelHistoricoEvolucaoEnfermagemRepositorio _relHistoricoEvolucaoEnfermagemRepositorio;
		public A9ProntuarioTest() 
		{
			var services = new ServiceCollection();

			services.AddTransient<IA9ProntuarioRepositorio, A9ProntuarioRepositorio>();
			services.AddTransient<IA29AtendimentoRepositorio, A29AtendimentoRepositorio>();
			services.AddTransient<IA10RegistroEvolucaoEnfermagemRepositorio, A10RegistroEvolucaoEnfermagemRepositorio>();
			services.AddTransient<IRelHistoricoEvolucaoEnfermagemRepositorio, RelHistoricoEvolucaoEnfermagemRepositorio>();

			services.AddEntityFrameworkSqlServer()
				.AddDbContext<GestaoSaudeContext>(options => options.UseSqlServer(A1UsuarioTest.connectionString, b => b.MigrationsAssembly("TCC.GestaoSaude.DataAccess")));
			var serviceProvider = services.BuildServiceProvider();

			_prontuarioRepositorio = serviceProvider.GetService<IA9ProntuarioRepositorio>();
			_atendimentoRepositorio = serviceProvider.GetService<IA29AtendimentoRepositorio>();
			_registroEvolucaoEnfermagemRepositorio = serviceProvider.GetService<IA10RegistroEvolucaoEnfermagemRepositorio>();
			_relHistoricoEvolucaoEnfermagemRepositorio = serviceProvider.GetService<IRelHistoricoEvolucaoEnfermagemRepositorio>();
		}

		[Fact]
		public void CadastrarProntuarioTest()
		{
			var atendimento = new A29AtendimentoBusiness(_atendimentoRepositorio,_prontuarioRepositorio,_registroEvolucaoEnfermagemRepositorio).BuscarAtendimento(1);

			A9Prontuario prontuario = new A9Prontuario();
			prontuario.A9ProntuarioInternado = false;

			prontuario.RelAtendimentoProntuario = new List<RelAtendimentoProntuario>();
			prontuario.RelAtendimentoProntuario.Add(new RelAtendimentoProntuario() { A29AtendimentoId = atendimento.A29AtendimentoId });

			prontuario.RelHistoricoEvolucaoEnfermagem = new List<RelHistoricoEvolucaoEnfermagem>();

			A10RegistroEvolucaoEnfermagem registroEnfermagem = new A10RegistroEvolucaoEnfermagem();
			registroEnfermagem.A10RegistroEvolucaoEnfermagemData = DateTime.Now;
			registroEnfermagem.A10RegistroEvolucaoEnfermagemDescrição = "O Paciente esta com febre alta";
			registroEnfermagem.A10RegistroEvolucaoEnfermagemHora = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
			registroEnfermagem.A13ProfissionalCodigoCns = "1";

			A10RegistroEvolucaoEnfermagem registroEnfermagem2 = new A10RegistroEvolucaoEnfermagem();
			registroEnfermagem2.A10RegistroEvolucaoEnfermagemData = DateTime.Now;
			registroEnfermagem2.A10RegistroEvolucaoEnfermagemDescrição = "O Paciente esta manchas avermelhadas";
			registroEnfermagem2.A10RegistroEvolucaoEnfermagemHora = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
			registroEnfermagem2.A13ProfissionalCodigoCns = "1";

			List<A10RegistroEvolucaoEnfermagem> lstRegistros = new List<A10RegistroEvolucaoEnfermagem>();
			lstRegistros.Add(registroEnfermagem);
			lstRegistros.Add(registroEnfermagem2);

			List<int> registrosCadastrados = new A10RegistroEvolucaoEnfermagemBusiness(_registroEvolucaoEnfermagemRepositorio).CadastrarRegistrosEnfermagem(lstRegistros);

			foreach (var item in registrosCadastrados)
			{
				prontuario.RelHistoricoEvolucaoEnfermagem.Add(new RelHistoricoEvolucaoEnfermagem() { A10RegistroEvolucaoEnfermagemId = item });
			}

			var resultado = new A9ProntuarioBusiness(_prontuarioRepositorio,null,null).CadastrarProntuario(prontuario);

			Assert.True(resultado);
		}


		[Fact]
		public void BuscarProntuarioPorCodigoTest() 
		{
			int idProntuario = _prontuarioRepositorio.GetAll().FirstOrDefault().A9ProntuarioId;
			var prontuario = new A9ProntuarioBusiness(_prontuarioRepositorio,_registroEvolucaoEnfermagemRepositorio,null).BuscarProntuarioPorCodigo(idProntuario);

			Assert.True(prontuario != null);
		}

		[Fact]
		public void AtualizarProntuarioTest()
		{
			int idProntuario = _prontuarioRepositorio.GetAll().FirstOrDefault().A9ProntuarioId;
			var prontuario = new A9ProntuarioBusiness(_prontuarioRepositorio, _registroEvolucaoEnfermagemRepositorio, null).BuscarProntuarioPorCodigo(idProntuario);

			prontuario.A9ProntuarioCondutaTerapeuta = "A terapia necessaria para a recuperação do paciente é acupuntura. O paciente precisa fazer fisioterapia";
			prontuario.A9ProntuarioDescricaoCirurgica = "Cirurgia do tendão.";
			prontuario.A9ProntuarioPrescricaoMedica = "Tomar dipirona";

			A10RegistroEvolucaoEnfermagem novoRegistro = new A10RegistroEvolucaoEnfermagem();
			novoRegistro.A10RegistroEvolucaoEnfermagemData = DateTime.Now;
			novoRegistro.A10RegistroEvolucaoEnfermagemDescrição = "A paciente apresentou melhora após medicação";
			novoRegistro.A10RegistroEvolucaoEnfermagemHora = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
			novoRegistro.A13ProfissionalCodigoCns = "700204985949127";
			List<A10RegistroEvolucaoEnfermagem> lstNovoRegistros = new List<A10RegistroEvolucaoEnfermagem>();
			lstNovoRegistros.Add(novoRegistro);

			var resultado = new A9ProntuarioBusiness(_prontuarioRepositorio, _registroEvolucaoEnfermagemRepositorio, _relHistoricoEvolucaoEnfermagemRepositorio).AtualizarProntuario(prontuario, lstNovoRegistros);

			Assert.True(resultado && prontuario.Mensagens.ToList().Where(c => c.TipoMensagem == Common.TipoMensagem.Sucesso).Count() > 0);
		}
	}
}
