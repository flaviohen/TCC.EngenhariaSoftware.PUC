using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.Common;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.DataAccess.Repositorio;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.Business
{
	public class A9ProntuarioBusiness
	{
		private readonly IA9ProntuarioRepositorio _prontuarioRepositorio;
		private readonly IA13ProfissionalRepositorio _profissionalRepositorio;
		private readonly IA10RegistroEvolucaoEnfermagemRepositorio _registroEvolucaoEnfermagemRepositorio;
		private readonly IRelHistoricoEvolucaoEnfermagemRepositorio _relHistoricoEvolucaoEnfermagemRepositorio;
		private readonly IA2UsuarioInternoRepositorio _usuarioInternoRepositorio;

		public A9ProntuarioBusiness(IA9ProntuarioRepositorio prontuarioRepositorio, 
			IA10RegistroEvolucaoEnfermagemRepositorio registroEvolucaoEnfermagemRepositorio, 
			IRelHistoricoEvolucaoEnfermagemRepositorio relHistoricoEvolucaoEnfermagemRepositorio,
			IA13ProfissionalRepositorio profissionalRepositorio,
			IA2UsuarioInternoRepositorio usuarioInternoRepositorio) 
		{
			_prontuarioRepositorio = prontuarioRepositorio;
			_registroEvolucaoEnfermagemRepositorio = registroEvolucaoEnfermagemRepositorio;
			_relHistoricoEvolucaoEnfermagemRepositorio = relHistoricoEvolucaoEnfermagemRepositorio;
			_profissionalRepositorio = profissionalRepositorio;
			_usuarioInternoRepositorio = usuarioInternoRepositorio;
		}

		public bool CadastrarProntuario(A9Prontuario prontuario) 
		{
			try
			{
				_prontuarioRepositorio.Add(prontuario);
				_prontuarioRepositorio.Save();
				return prontuario.A9ProntuarioId > 0;
			}
			catch (Exception ex)
			{
				prontuario.Mensagens = new List<Common.Mensagem>();
				prontuario.Mensagens.Add(Util.AdicionarMensagem(TipoMensagem.Erro, ex.Message));
				return false;
			}
		}

		public A9Prontuario BuscarProntuarioPorCodigo(int idProntuario) 
		{
			A9Prontuario prontuario = null;
			try
			{
				List<string> includes = new List<string>();
				includes.Add("RelHistoricoEvolucaoEnfermagem");
				includes.Add("RelHistoricoEvolucaoMedicaDiaria");
				includes.Add("RelHistoricoExamePaciente");
				includes.Add("RelProntuarioRecomendacaoMedica");
				prontuario = _prontuarioRepositorio.Find(c => c.A9ProntuarioId == idProntuario, includes);

				if (prontuario == null)
				{
					prontuario = new A9Prontuario();
					prontuario.Mensagens = new List<Mensagem>();
					prontuario.Mensagens.Add(Util.AdicionarMensagem(TipoMensagem.Atencao, Common.MensagensSistema.MsgsSistema.MsgProntuarioNaoEncontrado));
				}
				else 
				{
					if (prontuario.RelHistoricoEvolucaoEnfermagem.Count > 0) 
					{
						foreach (var item in prontuario.RelHistoricoEvolucaoEnfermagem)
						{
							item.A10RegistroEvolucaoEnfermagem = new A10RegistroEvolucaoEnfermagemBusiness(_registroEvolucaoEnfermagemRepositorio, _profissionalRepositorio,_usuarioInternoRepositorio).BuscarRegistroEvolucaoEnfermagemPorCodigo(item.A10RegistroEvolucaoEnfermagemId);
						}
					}
				}
				return prontuario;
			}
			catch (Exception ex)
			{
				prontuario = new A9Prontuario();
				prontuario.Mensagens = new List<Mensagem>();
				prontuario.Mensagens.Add(Util.AdicionarMensagem(TipoMensagem.Erro, ex.Message));
				return prontuario;
			}
		}

		public bool AtualizarProntuario(A9Prontuario prontuario, List<A10RegistroEvolucaoEnfermagem> lstNovosRegistroEvolucaoEnfermagem) 
		{
			try
			{
				List<int> novosRegistroAdicionado = new List<int>();
				_prontuarioRepositorio.Update(prontuario, prontuario.A9ProntuarioId);
				if (lstNovosRegistroEvolucaoEnfermagem.Count > 0) 
				{
					novosRegistroAdicionado = new A10RegistroEvolucaoEnfermagemBusiness(_registroEvolucaoEnfermagemRepositorio, _profissionalRepositorio,_usuarioInternoRepositorio).CadastrarRegistrosEnfermagem(lstNovosRegistroEvolucaoEnfermagem);
				}

				foreach (var item in novosRegistroAdicionado)
				{
					RelHistoricoEvolucaoEnfermagem relEvolucaoEnfermagem = new RelHistoricoEvolucaoEnfermagem();
					relEvolucaoEnfermagem.A10RegistroEvolucaoEnfermagemId = item;
					relEvolucaoEnfermagem.A9ProntuarioId = prontuario.A9ProntuarioId;
					_relHistoricoEvolucaoEnfermagemRepositorio.Add(relEvolucaoEnfermagem);
					_relHistoricoEvolucaoEnfermagemRepositorio.Save();
				}

				prontuario.Mensagens.Add(Util.AdicionarMensagem(TipoMensagem.Sucesso, Common.MensagensSistema.MsgsSistema.MsgProntuarioAtualizado));
				return true;
			}
			catch (Exception ex)
			{
				prontuario.Mensagens.Add(Util.AdicionarMensagem(TipoMensagem.Erro, ex.Message));
				return false;
			}
		}
	}
}
