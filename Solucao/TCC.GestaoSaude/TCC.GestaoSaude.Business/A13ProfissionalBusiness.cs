using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCC.GestaoSaude.Common;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.Business
{
	
	public class A13ProfissionalBusiness
	{
		private readonly IA13ProfissionalRepositorio _profissionalRepositorio;
		private readonly IA2UsuarioInternoRepositorio _usuarioInternoRepositorio;
		public A13ProfissionalBusiness(IA13ProfissionalRepositorio profissionalRepositorio, IA2UsuarioInternoRepositorio usuarioInternoRepositorio) 
		{
			_profissionalRepositorio = profissionalRepositorio;
			_usuarioInternoRepositorio = usuarioInternoRepositorio;
		}

		public A13Profissional CadastrarProfissional(A13Profissional profissional) 
		{
			try
			{
				var usuarioExistente = _profissionalRepositorio.Find(c => c.A13ProfissionalCodigoCns == profissional.A13ProfissionalCodigoCns);
				if (usuarioExistente == null)
				{
					_profissionalRepositorio.Add(profissional);
					_profissionalRepositorio.Save();

				}
				else 
				{
					Mensagem msg = new Mensagem();
					msg.TipoMensagem = TipoMensagem.Atencao;
					msg.DescricaoMensagem = Common.MensagensSistema.MsgsSistema.MsgProfissionalExistente;

					profissional.Mensagens = new List<Mensagem>();
					profissional.Mensagens.Add(msg);
				}
				return profissional;
			}

			catch (Exception ex)
			{
				Mensagem msg = new Mensagem();
				msg.TipoMensagem = TipoMensagem.Erro;
				msg.DescricaoMensagem = ex.Message;

				profissional.Mensagens = new List<Mensagem>();
				profissional.Mensagens.Add(msg);
				return profissional;
			}
		}

		public A13Profissional RetornarProfissional(string codigoCNS)
		{
			try
			{
				List<string> includes = new List<string>();
				includes.Add("RelUsuarioInternoProfissional");
				A13Profissional profissional = _profissionalRepositorio.Find(c => c.A13ProfissionalCodigoCns.Equals(codigoCNS), includes);
				if (profissional == null)
				{
					profissional = new A13Profissional();
					Mensagem msg = new Mensagem();
					msg.TipoMensagem = TipoMensagem.Atencao;
					msg.DescricaoMensagem = Common.MensagensSistema.MsgsSistema.MsgProfissionalExistente;
					profissional.Mensagens.Add(msg);
				}
				else 
				{
					int idUsuarioInterno = profissional.RelUsuarioInternoProfissional.ToList()[0].A2UsuarioInternoId;
					profissional.RelUsuarioInternoProfissional.ToList()[0].A2UsuarioInterno = new A2UsuarioInternoBusiness(_usuarioInternoRepositorio).BuscarUsuarioInternoPorID(idUsuarioInterno);
				}
				return profissional;
			}
			catch (Exception ex)
			{
				A13Profissional profissional = new A13Profissional();
				Mensagem msg = new Mensagem();
				msg.TipoMensagem = TipoMensagem.Erro;
				msg.DescricaoMensagem = ex.Message;
				profissional.Mensagens.Add(msg);
				return profissional;
			}
		}
	}
}
