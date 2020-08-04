using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TCC.GestaoSaude.Business;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;
using TCC.GestaoSaude.View.Models;
using TCC.GestaoSaude.View.SessionCustom;

namespace TCC.GestaoSaude.View.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IA1UsuarioRepositorio _usuarioRepositorio;
		private readonly IA2UsuarioInternoRepositorio _usuarioInternoRepositorio;
		private readonly IA6PerfilRepositorio _perfilRepositorio;
		private readonly IA13ProfissionalRepositorio _profissionalRepositorio;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private Sessao Sessao;

		public HomeController(ILogger<HomeController> logger, IA1UsuarioRepositorio usuarioRepositorio, IA2UsuarioInternoRepositorio usuarioInternoRepositorio, IA6PerfilRepositorio perfilRepositorio, IA13ProfissionalRepositorio profissionalRepositorio, IHttpContextAccessor httpContextAccessor)
		{
			_logger = logger;
			_usuarioRepositorio = usuarioRepositorio;
			_usuarioInternoRepositorio = usuarioInternoRepositorio;
			_perfilRepositorio = perfilRepositorio;
			_profissionalRepositorio = profissionalRepositorio;
			_httpContextAccessor = httpContextAccessor;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(string txtCPF, string txtSenha)
		{
			Sessao = new Sessao(_httpContextAccessor);
			A1UsuarioBusiness usuarioNegocio = new A1UsuarioBusiness(_usuarioRepositorio, _usuarioInternoRepositorio, _perfilRepositorio, _profissionalRepositorio);
			A1Usuario usuario = new A1Usuario();
			usuario.A1UsuarioNumeroCpf = txtCPF;
			usuario.A1UsuarioSenha = txtSenha;

			if (usuarioNegocio.Logar(usuario))
			{	
				Sessao.UsuarioExterno = usuarioNegocio.BuscarUsuarioPorCPF(usuario.A1UsuarioNumeroCpf);
				return RedirectToAction("PaginaInicial", "Painel");
			}
			else 
			{
				Sessao.UsuarioExterno = null;
				Sessao.UsuarioInterno = null;
				ViewBag.Mensagem = usuario.Mensagens[0].DescricaoMensagem;
				return View();
			}
		}

		[HttpPost]
		public IActionResult LoginInterno(string txtEmail, string txtSenhaInterno)
		{
			Sessao = new Sessao(_httpContextAccessor);
			A1UsuarioBusiness usuarioNegocio = new A1UsuarioBusiness(_usuarioRepositorio, _usuarioInternoRepositorio, _perfilRepositorio, _profissionalRepositorio);
			A2UsuarioInternoBusiness usuarioInternoNegocio = new A2UsuarioInternoBusiness(_usuarioInternoRepositorio);
			A2UsuarioInterno usuario = new A2UsuarioInterno();
			usuario.A2UsuarioInternoEmail = txtEmail;
			usuario.A2UsuarioInternoSenha = txtSenhaInterno;

			if (usuarioNegocio.LogarInterno(usuario))
			{
				Sessao.UsuarioInterno = usuarioInternoNegocio.BuscarUsuarioInternoPorEmail(usuario.A2UsuarioInternoEmail);
				return RedirectToAction("PaginaInicial", "Painel");
			}
			else
			{
				Sessao.UsuarioInterno = null;
				Sessao.UsuarioExterno = null;
				ViewBag.Mensagem = usuario.Mensagens[0].DescricaoMensagem;
				return View("Index","Home");
			}
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
