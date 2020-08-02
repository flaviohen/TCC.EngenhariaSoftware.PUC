using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TCC.GestaoSaude.View.SessionCustom;

namespace TCC.GestaoSaude.View.Controllers
{
	public class PainelController : Controller
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private Sessao _sessao;
		public PainelController(IHttpContextAccessor httpContextAccessor) 
		{
			_httpContextAccessor = httpContextAccessor;
			_sessao = new Sessao(httpContextAccessor);
		}

		public IActionResult PaginaInicial()
		{
			if (_sessao.UsuarioExterno != null) 
			{
				ViewBag.NomeUsuario = _sessao.UsuarioExterno.A1UsuarioNome;
				string perfis = "";
				foreach (var item in _sessao.UsuarioExterno.RelUsuarioPerfil)
				{
					perfis += item.A6Perfil.A6PerfilDescricao + " / "; 
				}
				ViewBag.Perfil = perfis;
			}

			if (_sessao.UsuarioInterno != null)
			{
				ViewBag.NomeUsuario = _sessao.UsuarioInterno.A2UsuarioInternoNome;
				string perfis = "";
				foreach (var item in _sessao.UsuarioInterno.RelUsuarioInternoPerfil)
				{
					perfis += item.A6Perfil.A6PerfilDescricao + " / ";
				}
				ViewBag.Perfil = perfis;
			}
			return View();
		}

		public IActionResult Logout() 
		{
			_sessao.UsuarioExterno = null;
			_sessao.UsuarioInterno = null;
			return RedirectToAction("Index", "Home");
		}
	}
}
