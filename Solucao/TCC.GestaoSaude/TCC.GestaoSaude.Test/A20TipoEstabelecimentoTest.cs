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
	public class A20TipoEstabelecimentoTest
	{
		
		private readonly IA20TipoEstabelecimentoRepositorio _tipoEstabelecimentoRepositorio;
		public A20TipoEstabelecimentoTest() 
		{
			var services = new ServiceCollection();

			services.AddTransient<IA20TipoEstabelecimentoRepositorio, A20TipoEstabelecimentoRepositorio>();

			services.AddEntityFrameworkSqlServer()
				.AddDbContext<GestaoSaudeContext>(options => options.UseSqlServer(A1UsuarioTest.connectionString, b => b.MigrationsAssembly("TCC.GestaoSaude.DataAccess")));
			var serviceProvider = services.BuildServiceProvider();

			_tipoEstabelecimentoRepositorio = serviceProvider.GetService<IA20TipoEstabelecimentoRepositorio>();
		}

		[Fact]
		public void CadastrarTipoEstabelecimentoTest()
		{
			A20TipoEstabelecimento tipoEstabelecimento = new A20TipoEstabelecimento();
			tipoEstabelecimento.A20TipoEstabelecimentoCodigo = "0";
			tipoEstabelecimento.A20TipoEstabelecimentoDescricao = "OUTROS";

			A20TipoEstabelecimento tipoEstabelecimento2 = new A20TipoEstabelecimento();
			tipoEstabelecimento2.A20TipoEstabelecimentoCodigo = "1";
			tipoEstabelecimento2.A20TipoEstabelecimentoDescricao = "UNIDADE BASICA DE SAUDE";

			var resultado1 = new A20TipoEstabelecimentoBusiness(_tipoEstabelecimentoRepositorio).CadastrarTipoEstabelecimento(tipoEstabelecimento);
			var resultado2 = new A20TipoEstabelecimentoBusiness(_tipoEstabelecimentoRepositorio).CadastrarTipoEstabelecimento(tipoEstabelecimento2);

			Assert.True(resultado1 && resultado2);
		}

		[Fact]
		public void RetornarTiposEstabelecimentoTest() 
		{
			var tiposEstabelecimento = new A20TipoEstabelecimentoBusiness(_tipoEstabelecimentoRepositorio).RetornarTiposEstabelecimento();
			Assert.True(tiposEstabelecimento.Count > 0);
		}

		

	}
}
