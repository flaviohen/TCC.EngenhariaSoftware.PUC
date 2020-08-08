using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.Common;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.Business
{
	public class A10RegistroEvolucaoEnfermagemBusiness
	{
		private readonly IA10RegistroEvolucaoEnfermagemRepositorio _registroEvolucaoEnfermagem;
		private readonly IA13ProfissionalRepositorio _profissionalRepositorio;
		private readonly IA2UsuarioInternoRepositorio _usuarioInternoRepositorio;
		public A10RegistroEvolucaoEnfermagemBusiness(
			IA10RegistroEvolucaoEnfermagemRepositorio registroEvolucaoEnfermagem, 
			IA13ProfissionalRepositorio profissionalRepositorio,
			IA2UsuarioInternoRepositorio usuarioInternoRepositorio) 
		{
			_registroEvolucaoEnfermagem = registroEvolucaoEnfermagem;
			_profissionalRepositorio = profissionalRepositorio;
			_usuarioInternoRepositorio = usuarioInternoRepositorio;
		}

		public List<int> CadastrarRegistrosEnfermagem(List<A10RegistroEvolucaoEnfermagem> registrosEnfermagem) 
		{
			List<int> lstIdRegistro = new List<int>();
			try
			{
				foreach (var item in registrosEnfermagem)
				{
					_registroEvolucaoEnfermagem.Add(item);
					_registroEvolucaoEnfermagem.Save();
					lstIdRegistro.Add(item.A10RegistroEvolucaoEnfermagemId);
				}
				return lstIdRegistro;
			}
			catch (Exception)
			{
				return lstIdRegistro = null;
			}
		}

		public A10RegistroEvolucaoEnfermagem BuscarRegistroEvolucaoEnfermagemPorCodigo(int idRegistro) 
		{
			A10RegistroEvolucaoEnfermagem registro = null;
			try
			{
				registro = _registroEvolucaoEnfermagem.Get(idRegistro);
				if (registro == null)
				{
					registro = new A10RegistroEvolucaoEnfermagem();
					registro.Mensagens.Add(Util.AdicionarMensagem(TipoMensagem.Atencao, string.Format(Common.MensagensSistema.MsgsSistema.MsgRegistroEvolucaEnfermagemNaoEncontrado, idRegistro)));
				}
				else 
				{
					registro.A13ProfissionalCodigoCnsNavigation = new A13ProfissionalBusiness(_profissionalRepositorio,_usuarioInternoRepositorio).RetornarProfissional(registro.A13ProfissionalCodigoCns);
				}

				return registro;
			}
			catch (Exception ex)
			{
				registro = new A10RegistroEvolucaoEnfermagem();
				registro.Mensagens.Add(Util.AdicionarMensagem(TipoMensagem.Erro, ex.Message));
				return registro;
			}
		}
	}
}
