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
		UsuarioInterno,
		RegistroEvolucaoEnfermagem,
		IDProntuario,
		IDAtendimento
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

		public List<RegistroEnfermagemViewModel> RegistrosEvolucaoEnfermagem 
		{
			get
			{
				return _httpContextAccessor.HttpContext.Session.GetObjectFromJson<List<RegistroEnfermagemViewModel>>(NomeSessao.RegistroEvolucaoEnfermagem.ToString());
			}

			set
			{
				_httpContextAccessor.HttpContext.Session.SetObjectAsJson(NomeSessao.RegistroEvolucaoEnfermagem.ToString(), value);
			}
		}

		public int IDProntuario 
		{
			get
			{
				return _httpContextAccessor.HttpContext.Session.GetObjectFromJson<int>(NomeSessao.IDProntuario.ToString());
			}

			set
			{
				_httpContextAccessor.HttpContext.Session.SetObjectAsJson(NomeSessao.IDProntuario.ToString(), value);
			}
		}

		public int IDAtendimento 
		{
			get
			{
				return _httpContextAccessor.HttpContext.Session.GetObjectFromJson<int>(NomeSessao.IDAtendimento.ToString());
			}

			set
			{
				_httpContextAccessor.HttpContext.Session.SetObjectAsJson(NomeSessao.IDAtendimento.ToString(), value);
			}
		}

		public static bool ValidarExistirUsuarioSessao(IHttpContextAccessor httpContextAccessor)
		{
			Sessao sessi = new Sessao(httpContextAccessor);
			return sessi.UsuarioExterno != null || sessi.UsuarioInterno != null;
		}

		public static InformacaoUsuario RetornarInformacaoUsuario(Sessao sessao) 
		{
			InformacaoUsuario informacao = new InformacaoUsuario();
			if (sessao.UsuarioExterno != null)
			{
				informacao.NomeUsuario = sessao.UsuarioExterno.A1UsuarioNome;
				string perfis = "";
				foreach (var item in sessao.UsuarioExterno.RelUsuarioPerfil)
				{
					perfis += item.A6Perfil.A6PerfilDescricao + " / ";
				}
				informacao.Perfis = perfis;
			}

			if (sessao.UsuarioInterno != null)
			{
				informacao.NomeUsuario = sessao.UsuarioInterno.A2UsuarioInternoNome;
				string perfis = "";
				foreach (var item in sessao.UsuarioInterno.RelUsuarioInternoPerfil)
				{
					perfis += item.A6Perfil.A6PerfilDescricao + " / ";
				}
				informacao.Perfis = perfis;
			}

			return informacao;
		}
	}

	public class InformacaoUsuario 
	{
		public string NomeUsuario { get; set; }
		public string Perfis { get; set; }
	}
}
