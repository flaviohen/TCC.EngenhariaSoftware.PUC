using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.Common;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.Business
{
	
	public class A13ProfissionalBusiness
	{
		private readonly IA13ProfissionalRepositorio _profissionalRepositorio;

		public A13ProfissionalBusiness(IA13ProfissionalRepositorio profissionalRepositorio) 
		{
			_profissionalRepositorio = profissionalRepositorio;
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
	}
}
