using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCC.GestaoSaude.Common;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.Business
{
	public class A20TipoEstabelecimentoBusiness
	{
		private readonly IA20TipoEstabelecimentoRepositorio _tipoEstabelecimentoRepositorio;

		public A20TipoEstabelecimentoBusiness(IA20TipoEstabelecimentoRepositorio tipoEstabelecimentoRepositorio) 
		{
			_tipoEstabelecimentoRepositorio = tipoEstabelecimentoRepositorio;
		}

		public bool CadastrarTipoEstabelecimento(A20TipoEstabelecimento tipoEstabelecimento) 
		{
			try
			{
				_tipoEstabelecimentoRepositorio.Add(tipoEstabelecimento);
				_tipoEstabelecimentoRepositorio.Save();
				return tipoEstabelecimento.A20TipoEstabelecimentoId > 0;
			}
			catch (Exception ex)
			{
				tipoEstabelecimento.Mensagens.Add(Util.AdicionarMensagem(TipoMensagem.Erro, ex.Message));
				return false;
			}
		}

		public List<A20TipoEstabelecimento> RetornarTiposEstabelecimento() 
		{
			try
			{
				return _tipoEstabelecimentoRepositorio.GetAll().ToList();
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}
