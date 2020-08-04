using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;
using TCC.GestaoSaude.Common;

namespace TCC.GestaoSaude.Business
{
	public class A1UsuarioBusiness
	{
		private readonly IA1UsuarioRepositorio _usuarioRepositorio;
		private readonly IA2UsuarioInternoRepositorio _usuarioInternoRepositorio;
		private readonly IA6PerfilRepositorio _perfilRepositorio;
		private readonly IA13ProfissionalRepositorio _profissionalRepositorio;
		public A1UsuarioBusiness(IA1UsuarioRepositorio usuarioRepositorio, IA2UsuarioInternoRepositorio usuarioInternoRepositorio, IA6PerfilRepositorio perfilRepositorio, IA13ProfissionalRepositorio profissionalRepositorio)
		{
			_usuarioRepositorio = usuarioRepositorio;
			_usuarioInternoRepositorio = usuarioInternoRepositorio;
			_perfilRepositorio = perfilRepositorio;
			_profissionalRepositorio = profissionalRepositorio;
		}

		public bool LogarInterno(A2UsuarioInterno usuarioInterno)
		{
			if (Autenticar(null, usuarioInterno, Enumeradores.ModoAutenticacao.LogarInterno))
				return true;
			else
				return false;
		}
		public bool Logar(A1Usuario usuario) 
		{
			if (Autenticar(usuario, null, Enumeradores.ModoAutenticacao.LogarPaciente))
				return true;
			else
				return false;
		}

		public bool CriarLogin(A1Usuario usuario) 
		{
			bool retorno = true;
			if (Autenticar(usuario, null, Enumeradores.ModoAutenticacao.CriarLogin))
			{
				_usuarioRepositorio.Add(usuario);
				_usuarioRepositorio.Save();

				retorno = usuario.A1UsuarioId > 0;
			}
			else 
			{
				retorno = false;
			}

			return retorno;
		}

		private bool Autenticar(A1Usuario usuarioPaciente, A2UsuarioInterno usuarioInterno, Enumeradores.ModoAutenticacao modoAutenticacao) 
		{
			
			Mensagem mensagem = new Mensagem();
			mensagem.TipoMensagem = TipoMensagem.Atencao;
			bool retorno = true;
			switch (modoAutenticacao)
			{
				
				case Enumeradores.ModoAutenticacao.CriarLogin:
					var usuarioExistente = _usuarioRepositorio.Find(c => c.A1UsuarioNumeroCpf == usuarioPaciente.A1UsuarioNumeroCpf);
					if (usuarioExistente != null) 
					{
						mensagem.DescricaoMensagem = Common.MensagensSistema.MsgsSistema.MsgUsuarioExistente;
						usuarioPaciente.Mensagens.Add(mensagem);
						retorno = false;
					}
					break;
				case Enumeradores.ModoAutenticacao.LogarInterno:
					List<string> includes = new List<string>();
					includes.Add("RelUsuarioInternoPerfil");
					includes.Add("RelUsuarioInternoProfissional");

					var usuarioInternoExistente = _usuarioInternoRepositorio.Find(c => c.A2UsuarioInternoEmail == usuarioInterno.A2UsuarioInternoEmail, includes);
					if (usuarioInternoExistente == null) 
					{
						mensagem.DescricaoMensagem = Common.MensagensSistema.MsgsSistema.MsgUsuarioInternoNaoExiste;
						usuarioInterno.Mensagens.Add(mensagem);
						retorno = false;
					}
					if (usuarioInternoExistente != null) 
					{
						if (usuarioInterno.A2UsuarioInternoSenha != usuarioInternoExistente.A2UsuarioInternoSenha)
						{
							mensagem.DescricaoMensagem = Common.MensagensSistema.MsgsSistema.MsgSenhaIncorreta;
							usuarioInterno.Mensagens.Add(mensagem);
							retorno = false;
						}
						else 
						{
							usuarioInterno.RelUsuarioInternoPerfil = usuarioInternoExistente.RelUsuarioInternoPerfil;
							usuarioInterno.RelUsuarioInternoProfissional = usuarioInternoExistente.RelUsuarioInternoProfissional;

							foreach (var item in usuarioInterno.RelUsuarioInternoPerfil)
							{
								item.A6Perfil = _perfilRepositorio.Find(c => c.A6PerfilId == item.A6PerfilId);
							}
							foreach (var item in usuarioInterno.RelUsuarioInternoProfissional)
							{
								item.A13ProfissionalCodigoCnsNavigation = _profissionalRepositorio.Find(c => c.A13ProfissionalCodigoCns == item.A13ProfissionalCodigoCns);
							}
						}
					}
					break;
				case Enumeradores.ModoAutenticacao.LogarPaciente:

					List<string> includesPaciente = new List<string>();
					includesPaciente.Add("RelUsuarioPerfil");
					var usuarioLogar = _usuarioRepositorio.Find(c => c.A1UsuarioNumeroCpf == usuarioPaciente.A1UsuarioNumeroCpf, includesPaciente);
					if (usuarioLogar == null) 
					{
						mensagem.DescricaoMensagem = Common.MensagensSistema.MsgsSistema.MsgUsuarioNaoExiste;
						usuarioPaciente.Mensagens.Add(mensagem);
						retorno = false;
					}
					
					if (usuarioLogar != null)
					{
						if (usuarioPaciente.A1UsuarioSenha != usuarioLogar.A1UsuarioSenha)
						{
							mensagem.DescricaoMensagem = Common.MensagensSistema.MsgsSistema.MsgSenhaIncorreta;
							usuarioPaciente.Mensagens.Add(mensagem);
							retorno = false;
						}
						else 
						{
							usuarioPaciente.RelUsuarioPerfil = usuarioLogar.RelUsuarioPerfil;

							foreach (var item in usuarioPaciente.RelUsuarioPerfil)
							{
								item.A6Perfil = _perfilRepositorio.Find(c => c.A6PerfilId == item.A6PerfilId);
							}
						}
					}
					break;
				default:
					break;
			}

			return retorno;
		}

		public  A1Usuario BuscarUsuarioPorCPF(string numeroCpf)
		{
			A1Usuario usuario = new A1Usuario();
			try
			{
				List<string> includes = new List<string>();
				includes.Add("A3InformacaoCadastro");
				var usuarioPesquisado = _usuarioRepositorio.Find(c => c.A1UsuarioNumeroCpf == numeroCpf, includes);
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
				usuario = new A1Usuario();
				usuario.Mensagens = new List<Mensagem>();
				usuario.Mensagens.Add(Util.AdicionarMensagem(TipoMensagem.Erro, ex.Message));
				return usuario;
			}
		}
	}
}
