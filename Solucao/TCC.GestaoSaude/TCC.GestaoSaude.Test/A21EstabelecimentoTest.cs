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
	public class A21EstabelecimentoTest
	{
		
		private readonly IA21EstabelecimentoRepositorio _estabelecimentoRepositorio;
		private readonly IA20TipoEstabelecimentoRepositorio _tipoEstabelecimentoRepositorio;
		public A21EstabelecimentoTest() 
		{
			var services = new ServiceCollection();

			services.AddTransient<IA21EstabelecimentoRepositorio, A21EstabelecimentoRepositorio>();
			services.AddTransient<IA20TipoEstabelecimentoRepositorio, A20TipoEstabelecimentoRepositorio>();

			services.AddEntityFrameworkSqlServer()
				.AddDbContext<GestaoSaudeContext>(options => options.UseSqlServer(A1UsuarioTest.connectionString, b => b.MigrationsAssembly("TCC.GestaoSaude.DataAccess")));
			var serviceProvider = services.BuildServiceProvider();

			_estabelecimentoRepositorio = serviceProvider.GetService<IA21EstabelecimentoRepositorio>();
			_tipoEstabelecimentoRepositorio = serviceProvider.GetService<IA20TipoEstabelecimentoRepositorio>();
		}

		[Fact]
		public void CadastrarEstabelecimentoTest()
		{
			A21Estabelecimento estabelecimento = new A21Estabelecimento();
			estabelecimento.A20TipoEstabelecimento = new A20TipoEstabelecimentoBusiness(_tipoEstabelecimentoRepositorio).RetornarTiposEstabelecimento().FirstOrDefault();
			estabelecimento.A21EstabelecimentoBairro = "VALE DO PARAIBA";
			estabelecimento.A21EstabelecimentoCep = "27350000";
			estabelecimento.A21EstabelecimentoCnpj = "28695658000184";
			estabelecimento.A21EstabelecimentoCodigoEstabelecimento = "2289105";
			estabelecimento.A21EstabelecimentoCodigoUnidade = "RJ0000330040000000000000033634";
			estabelecimento.A21EstabelecimentoComplemento = "";
			estabelecimento.A21EstabelecimentoDataAtualizacao = new DateTime(2001,8,21);
			estabelecimento.A21EstabelecimentoDataAtualizacaoGeografica = DateTime.MinValue;
			estabelecimento.A21EstabelecimentoDataAtualizacaoOrigem = new DateTime(2002, 9, 2);
			estabelecimento.A21EstabelecimentoEmail = "fmsbm@uol.com.br";
			estabelecimento.A21EstabelecimentoEndereco = "RUA QUATRO";
			estabelecimento.A21EstabelecimentoFax = "(24)33227432";
			estabelecimento.A21EstabelecimentoLatitude = "";
			estabelecimento.A21EstabelecimentoLongitude = "";
			estabelecimento.A21EstabelecimentoNomeFantasia = "CONS ODONTOLOGICO COL MUN INDEPENDENCIA E LUZ";
			estabelecimento.A21EstabelecimentoNumero = "143";
			estabelecimento.A21EstabelecimentoRazaoSocial = "SECRETARIA MUNICIPAL DE SAUDE DE BARRA MANSA";
			estabelecimento.A21EstabelecimentoTelefone = "(24)33229192";

			var resultado = new A21EstabelecimentoBusiness(_estabelecimentoRepositorio).CadastrarEstabelecimento(estabelecimento);
			
			Assert.True(resultado);
		}

		[Fact]
		public void BuscarEstabelecimentoTest() 
		{
			var tipoEstabelecimento = _tipoEstabelecimentoRepositorio.GetAll().FirstOrDefault();
			var numeroCep = "27350000";
			var codigoEstabelecimento = "2289105";

			var lstEstabelecimentos = new A21EstabelecimentoBusiness(_estabelecimentoRepositorio).BuscarEstabelecimento(tipoEstabelecimento, numeroCep, codigoEstabelecimento);

			Assert.True(lstEstabelecimentos.Count > 0);
		}

		[Fact]
		public void ExcluirEstabelecimentoTest()
		{
			var estabelecimentoID = _estabelecimentoRepositorio.GetAll().First().A21EstabelecimentoId;

			var resultado = new A21EstabelecimentoBusiness(_estabelecimentoRepositorio).ExcluirEstabelecimento(estabelecimentoID);

			Assert.True(resultado);
		}

		[Fact]
		public void BuscarEstabelecimentoPorCodigoTest()
		{
			var estabelecimentoID = _estabelecimentoRepositorio.GetAll().First().A21EstabelecimentoId;

			var resultado = new A21EstabelecimentoBusiness(_estabelecimentoRepositorio).BuscarEstabelecimentoPorCodigo(estabelecimentoID);

			Assert.True(resultado != null);
		}

	}
}
