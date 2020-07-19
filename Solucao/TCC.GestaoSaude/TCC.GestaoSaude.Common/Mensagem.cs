using System;
using System.Collections.Generic;
using System.Text;

namespace TCC.GestaoSaude.Common
{
	public class Mensagem
	{
		public TipoMensagem TipoMensagem { get; set; }
		public string DescricaoMensagem { get; set; }
	}

	public enum TipoMensagem 
	{
		Erro = 1,
		Sucesso = 2,
		Atencao = 3
	}
}
