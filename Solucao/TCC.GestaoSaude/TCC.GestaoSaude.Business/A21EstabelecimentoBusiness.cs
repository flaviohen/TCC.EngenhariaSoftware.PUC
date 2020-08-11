using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCC.GestaoSaude.Common;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.Business
{
	public class A21EstabelecimentoBusiness
	{
		private readonly IA21EstabelecimentoRepositorio _estabelecimentoRepositorio;
		private readonly IA20TipoEstabelecimentoRepositorio _tipoEstabelecimentoRepositorio;
		public A21EstabelecimentoBusiness(IA21EstabelecimentoRepositorio estabelecimentoRepositorio, IA20TipoEstabelecimentoRepositorio tipoEstabelecimentoRepositorio)
		{
			_estabelecimentoRepositorio = estabelecimentoRepositorio;
			_tipoEstabelecimentoRepositorio = tipoEstabelecimentoRepositorio;
		}

		public bool CadastrarEstabelecimento(A21Estabelecimento estabelecimento)
		{
			try
			{
				var estabelecimentoExistente = _estabelecimentoRepositorio.Find(c => c.A21EstabelecimentoCodigoEstabelecimento == estabelecimento.A21EstabelecimentoCodigoEstabelecimento);
				if (estabelecimentoExistente == null)
				{
					_estabelecimentoRepositorio.Add(estabelecimento);
					_estabelecimentoRepositorio.Save();
					return estabelecimento.A21EstabelecimentoId > 0;
				}
				else
				{
					estabelecimento.Mensagens.Add(Util.AdicionarMensagem(TipoMensagem.Atencao, Common.MensagensSistema.MsgsSistema.MsgEstabelecimentoExistente));
					return false;
				}
			}
			catch (Exception ex)
			{
				estabelecimento.Mensagens.Add(Util.AdicionarMensagem(TipoMensagem.Erro, ex.Message));
				return false;
			}
		}

		public List<A21Estabelecimento> BuscarEstabelecimento(A20TipoEstabelecimento tipoEstabelecimento, string CepEstabelecimento, string codigoEstabelecimento)
		{
			try
			{
				List<A21Estabelecimento> resultado = new List<A21Estabelecimento>();
				if (tipoEstabelecimento != null && string.IsNullOrEmpty(CepEstabelecimento) && !string.IsNullOrEmpty(codigoEstabelecimento))
					resultado = _estabelecimentoRepositorio.FindAll(c => c.A20TipoEstabelecimento == tipoEstabelecimento && c.A21EstabelecimentoCodigoEstabelecimento == codigoEstabelecimento).ToList();
				if (tipoEstabelecimento!= null && !string.IsNullOrEmpty(CepEstabelecimento) && string.IsNullOrEmpty(codigoEstabelecimento))
					resultado = _estabelecimentoRepositorio.FindAll(c => c.A20TipoEstabelecimento == tipoEstabelecimento && c.A21EstabelecimentoCep == CepEstabelecimento).ToList();
				if (tipoEstabelecimento != null && !string.IsNullOrEmpty(CepEstabelecimento) && !string.IsNullOrEmpty(codigoEstabelecimento))
					resultado = _estabelecimentoRepositorio.FindAll(c => c.A20TipoEstabelecimento == tipoEstabelecimento && c.A21EstabelecimentoCep == CepEstabelecimento && c.A21EstabelecimentoCodigoEstabelecimento == codigoEstabelecimento).ToList();
				if ( tipoEstabelecimento != null && string.IsNullOrEmpty(CepEstabelecimento) && string.IsNullOrEmpty(codigoEstabelecimento))
					resultado = _estabelecimentoRepositorio.FindAll(c => c.A20TipoEstabelecimento == tipoEstabelecimento).ToList();
				if (tipoEstabelecimento == null && string.IsNullOrEmpty(CepEstabelecimento) && !string.IsNullOrEmpty(codigoEstabelecimento))
					resultado = _estabelecimentoRepositorio.FindAll(c => c.A21EstabelecimentoCodigoEstabelecimento == codigoEstabelecimento).ToList();
				if (tipoEstabelecimento == null && !string.IsNullOrEmpty(CepEstabelecimento) && string.IsNullOrEmpty(codigoEstabelecimento))
					resultado = _estabelecimentoRepositorio.FindAll(c => c.A21EstabelecimentoCep == CepEstabelecimento).ToList();
				if (tipoEstabelecimento == null && !string.IsNullOrEmpty(CepEstabelecimento) && !string.IsNullOrEmpty(codigoEstabelecimento))
					resultado = _estabelecimentoRepositorio.FindAll(c => c.A21EstabelecimentoCep == CepEstabelecimento && c.A21EstabelecimentoCodigoEstabelecimento == codigoEstabelecimento).ToList();
				
			    return resultado;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public bool ExcluirEstabelecimento(int idEstabelecimento)
		{
			try
			{
				var estabelecimento = _estabelecimentoRepositorio.Get(idEstabelecimento);
				if (estabelecimento != null)
				{
					_estabelecimentoRepositorio.Delete(estabelecimento);
					_estabelecimentoRepositorio.Save();
					return true;
				}
				{
					return false;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}

		public A21Estabelecimento BuscarEstabelecimentoPorCodigo(int idEstabelecimento)
		{
			A21Estabelecimento estabelecimento = null;
			try
			{
				estabelecimento = _estabelecimentoRepositorio.Get(idEstabelecimento);
				if (estabelecimento != null)
				{
					estabelecimento.A20TipoEstabelecimento = new A20TipoEstabelecimentoBusiness(_tipoEstabelecimentoRepositorio).RetornarTiposEstabelecimento().FirstOrDefault(c => c.A20TipoEstabelecimentoId == estabelecimento.A20TipoEstabelecimentoId);
					return estabelecimento;
				}
				else 
				{
					estabelecimento = new A21Estabelecimento();
					estabelecimento.Mensagens.Add(Util.AdicionarMensagem(TipoMensagem.Atencao, Common.MensagensSistema.MsgsSistema.MsgNaoExisteEstabelecimento));
					return estabelecimento;
				}	
			}
			catch (Exception ex)
			{
				estabelecimento = new A21Estabelecimento();
				estabelecimento.Mensagens.Add(Util.AdicionarMensagem(TipoMensagem.Erro, ex.Message));
				return estabelecimento;
			}
		}
	}
}
