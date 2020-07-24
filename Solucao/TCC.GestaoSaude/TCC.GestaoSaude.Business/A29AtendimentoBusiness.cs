using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCC.GestaoSaude.Common;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.Business
{
	public class A29AtendimentoBusiness
	{
		private readonly IA29AtendimentoRepositorio _atendimentoRepositorio;
		private readonly IA9ProntuarioRepositorio _prontuarioRepositorio;
		private readonly IA10RegistroEvolucaoEnfermagemRepositorio _registroEvolucaoEnfermagemRepositorio;
		public A29AtendimentoBusiness(IA29AtendimentoRepositorio atendimentoRepositorio, IA9ProntuarioRepositorio prontuarioRepositorio, IA10RegistroEvolucaoEnfermagemRepositorio registroEvolucaoEnfermagemRepositorio) 
		{
			_atendimentoRepositorio = atendimentoRepositorio;
			_prontuarioRepositorio = prontuarioRepositorio;
			_registroEvolucaoEnfermagemRepositorio = registroEvolucaoEnfermagemRepositorio;
		}

		public int CadastrarAtendimento(A29Atendimento atendimento) 
		{
			try
			{
				_atendimentoRepositorio.Add(atendimento);
				_atendimentoRepositorio.Save();

				return atendimento.A29AtendimentoId;
			}
			catch (Exception ex)
			{
				atendimento.Mensagens = new List<Common.Mensagem>();
				atendimento.Mensagens.Add(Util.AdicionarMensagem(TipoMensagem.Erro, ex.Message));
				return 0;			
			}
		}

		public A29Atendimento BuscarAtendimento(int codigoAtendimento) 
		{
			A29Atendimento atendimento = null;
			try
			{
				List<string> includes = new List<string>();
				includes.Add("A3InformacaoCadastro");
				includes.Add("RelAtendimentoProntuario");
				atendimento = _atendimentoRepositorio.Find(c => c.A29AtendimentoId == codigoAtendimento, includes);
				if (atendimento != null)
				{
					if (atendimento.RelAtendimentoProntuario.Count > 0) 
					{
						int idProntuario = atendimento.RelAtendimentoProntuario.ToList()[0].A9ProntuarioId;
						if (idProntuario > 0) 
						{
							atendimento.RelAtendimentoProntuario.ToList()[0].A9Prontuario = new A9ProntuarioBusiness(_prontuarioRepositorio, _registroEvolucaoEnfermagemRepositorio, null).BuscarProntuarioPorCodigo(idProntuario);
						} 
					}
					return atendimento;
				}
				else 
				{
					atendimento = new A29Atendimento();
					atendimento.Mensagens = new List<Mensagem>();
					atendimento.Mensagens.Add(Util.AdicionarMensagem(TipoMensagem.Atencao,Common.MensagensSistema.MsgsSistema.MsgAtendimentoNaoEncontrado));
					return atendimento;
				}	
			}
			catch (Exception ex)
			{
				atendimento = new A29Atendimento();
				atendimento.Mensagens = new List<Mensagem>();
				atendimento.Mensagens.Add(Util.AdicionarMensagem(TipoMensagem.Erro, ex.Message));
				return atendimento;
			}
		}
	}
}
