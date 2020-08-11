using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.GestaoSaude.Business;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.View.SessionCustom;

namespace TCC.GestaoSaude.View.Models.ViewComponents
{
	public class PainelViewComponent : ViewComponent
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private Sessao _sessao;
		private A6PerfilBusiness _perfilNegocio;
		private readonly IA6PerfilRepositorio _perfilRepositorio;
		public PainelViewComponent(IHttpContextAccessor httpContextAccessor, IA6PerfilRepositorio perfilRepositorio)
		{
			_httpContextAccessor = httpContextAccessor;
			_perfilRepositorio = perfilRepositorio;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			if (Sessao.ValidarExistirUsuarioSessao(_httpContextAccessor))
			{
				_sessao = new Sessao(_httpContextAccessor);
				_perfilNegocio = new A6PerfilBusiness(_perfilRepositorio);
				MenuViewModel menu = null;
				if (_sessao.UsuarioExterno != null)
				{
					var perfisUsuarioExterno = _perfilNegocio.RetornarPerfisUsuarioExterno(_sessao.UsuarioExterno);
					menu = new MenuViewModel(perfisUsuarioExterno);
				}
				if (_sessao.UsuarioInterno != null)
				{
					var perfisUsuarioInterno = _perfilNegocio.RetornarPerfisUsuarioInterno(_sessao.UsuarioInterno);
					menu = new MenuViewModel(perfisUsuarioInterno);
				}
				return View(menu);
			}
			else
			{
				return View("Index", "Home");
			}
		}
	}
}
