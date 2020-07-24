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
	public class EmailTest
	{
		
		private readonly IEmailEnvio _emailEnvio;
		public EmailTest() 
		{
			var services = new ServiceCollection();

			services.AddTransient<IEmailEnvio, EnvioEmail>();

			services.AddEntityFrameworkSqlServer()
				.AddDbContext<GestaoSaudeContext>(options => options.UseSqlServer(A1UsuarioTest.connectionString, b => b.MigrationsAssembly("TCC.GestaoSaude.DataAccess")));
			var serviceProvider = services.BuildServiceProvider();

			_emailEnvio = serviceProvider.GetService<IEmailEnvio>();
		}

		[Fact]
		public void EnviarEmailTest()
		{
			EmailModel emailRequest = new EmailModel();
			emailRequest.Assunto = "Senha Sistema de Saúde";
			emailRequest.DoEmail = "flavio.henriq@live.com";
			emailRequest.ParaEmail = "flavio.henriq@live.com";
			emailRequest.EmailsCopia = null;
			emailRequest.EmailsCopiaOculta = null;
			emailRequest.CorpoEmail = "<h1>Sua senha para acesso ao Sistema de Saude é 123456</h1>";

			Task result = new EmailBusiness(_emailEnvio).EnviarEmailAsync(emailRequest);

			Assert.True(result.IsCompletedSuccessfully);
		}	
	}
}
