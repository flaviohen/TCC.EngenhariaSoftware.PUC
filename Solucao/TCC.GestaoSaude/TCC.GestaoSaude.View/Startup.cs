using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TCC.GestaoSaude.Business;
using TCC.GestaoSaude.DataAccess.Contexto;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.DataAccess.Repositorio;

namespace TCC.GestaoSaude.View
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddEntityFrameworkSqlServer()
				.AddDbContext<GestaoSaudeContext>(options => options.UseSqlServer(Configuration.GetConnectionString("GestaoSaude"), b => b.MigrationsAssembly("TCC.GestaoSaude.View")));
			services.AddControllersWithViews();
			services.AddSession();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddTransient<IA1UsuarioRepositorio, A1UsuarioRepositorio>();
			services.AddTransient<IA10RegistroEvolucaoEnfermagemRepositorio, A10RegistroEvolucaoEnfermagemRepositorio>();
			services.AddTransient<IA13ProfissionalRepositorio, A13ProfissionalRepositorio>();
			services.AddTransient<IA20TipoEstabelecimentoRepositorio, A20TipoEstabelecimentoRepositorio>();
			services.AddTransient<IA21EstabelecimentoRepositorio, A21EstabelecimentoRepositorio>();
			services.AddTransient<IA29AtendimentoRepositorio, A29AtendimentoRepositorio>();
			services.AddTransient<IA2UsuarioInternoRepositorio, A2UsuarioInternoRepositorio>();
			services.AddTransient<IA6PerfilRepositorio, A6PerfilRepositorio>();
			services.AddTransient<IA9ProntuarioRepositorio, A9ProntuarioRepositorio>();
			services.AddTransient<IEmailEnvio, EnvioEmail>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();
			app.UseSession();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
