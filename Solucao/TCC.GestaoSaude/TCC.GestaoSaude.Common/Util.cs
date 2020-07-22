using System;
using System.Collections.Generic;
using System.Text;

namespace TCC.GestaoSaude.Common
{
	public class Util
	{
		public static Mensagem AdicionarMensagem(TipoMensagem tipo, string mensagen) 
		{
			Mensagem msg = new Mensagem();
			msg.TipoMensagem = tipo;
			msg.DescricaoMensagem = mensagen;

			return msg;
		}
	}
}
