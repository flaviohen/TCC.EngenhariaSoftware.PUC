using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.GestaoSaude.Models;
using TCC.GestaoSaude.View.Models;

namespace TCC.GestaoSaude.View.SessionCustom
{
	public enum NomeSessao
	{
		UsuarioExterno,
		UsuarioInterno
	}
	public class Sessao : Controller
	{
		public readonly IHttpContextAccessor _httpContextAccessor;

		public Sessao(IHttpContextAccessor httpContextAccessor) 
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public A1Usuario UsuarioExterno 
		{
			get 
			{
				return _httpContextAccessor.HttpContext.Session.GetObjectFromJson<A1Usuario>(NomeSessao.UsuarioExterno.ToString());
			}

			set 
			{
				_httpContextAccessor.HttpContext.Session.SetObjectAsJson(NomeSessao.UsuarioExterno.ToString(), value);
			}
		}

		public A2UsuarioInterno UsuarioInterno
		{
			get
			{
				return _httpContextAccessor.HttpContext.Session.GetObjectFromJson<A2UsuarioInterno>(NomeSessao.UsuarioInterno.ToString());
			}

			set
			{
				_httpContextAccessor.HttpContext.Session.SetObjectAsJson(NomeSessao.UsuarioInterno.ToString(), value);
			}
		}

		public static bool ValidarExistirUsuarioSessao(IHttpContextAccessor httpContextAccessor)
		{
			Sessao sessi = new Sessao(httpContextAccessor);
			return sessi.UsuarioExterno != null || sessi.UsuarioInterno != null;
		}
	}
}
