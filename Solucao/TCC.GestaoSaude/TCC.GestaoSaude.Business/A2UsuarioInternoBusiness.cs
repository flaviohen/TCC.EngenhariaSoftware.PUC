using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.Common;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.Business
{
	public class A2UsuarioInternoBusiness
	{
		private readonly IA2UsuarioInternoRepositorio _usuarioInternoRepositorio;

		public A2UsuarioInternoBusiness(IA2UsuarioInternoRepositorio usuarioInternoRepositorio) 
		{
			_usuarioInternoRepositorio = usuarioInternoRepositorio;
		}
		public bool CriarLoginInterno(A2UsuarioInterno usuario)
		{
			try
			{
				_usuarioInternoRepositorio.Add(usuario);
				_usuarioInternoRepositorio.Save();

				return usuario.A2UsuarioInternoId > 0;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public A2UsuarioInterno BuscarUsuarioInternoPorEmail(string email)
		{
			A2UsuarioInterno usuario = new A2UsuarioInterno();
			try
			{
				List<string> includes = new List<string>();
				includes.Add("RelUsuarioInternoPerfil");
				includes.Add("RelUsuarioInternoProfissional");
				var usuarioPesquisado = _usuarioInternoRepositorio.Find(c => c.A2UsuarioInternoEmail == email, includes);
				if (usuarioPesquisado == null)
				{
					usuario.Mensagens = new List<Mensagem>();
					usuario.Mensagens.Add(Util.AdicionarMensagem(TipoMensagem.Atencao, Common.MensagensSistema.MsgsSistema.MsgUsuarioNaoExiste));
					return usuario;
				}
				else
				{
					return usuarioPesquisado;
				}
			}
			catch (Exception ex)
			{
				usuario = new A2UsuarioInterno();
				usuario.Mensagens = new List<Mensagem>();
				usuario.Mensagens.Add(Util.AdicionarMensagem(TipoMensagem.Erro, ex.Message));
				return usuario;
			}
		}
	}
}
