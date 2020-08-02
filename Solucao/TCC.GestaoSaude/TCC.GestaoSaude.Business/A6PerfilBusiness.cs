using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TCC.GestaoSaude.Common;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.Business
{
	public class A6PerfilBusiness
	{
		private readonly IA6PerfilRepositorio _perfilRepositorio;

		public A6PerfilBusiness(IA6PerfilRepositorio perfilRepositorio) 
		{
			_perfilRepositorio = perfilRepositorio;
		}

		public async Task<bool> CadastrarPerfil(A6Perfil perfil) 
		{
			Mensagem msg = new Mensagem();
			try
			{
				var perfilExistente = _perfilRepositorio.Find(c => c.A6PerfilDescricao == perfil.A6PerfilDescricao);
				if (perfilExistente == null)
				{
					await _perfilRepositorio.AddAsyn(perfil);
					await _perfilRepositorio.SaveAsync();

					return perfil.A6PerfilId > 0;
				}
				else 
				{
					msg.TipoMensagem = TipoMensagem.Atencao;
					msg.DescricaoMensagem = Common.MensagensSistema.MsgsSistema.MsgPerfilExistente;
					perfil.Mensagens.Add(msg);
					return false;
				}
			}
			catch (Exception ex)
			{
				msg.TipoMensagem = TipoMensagem.Erro;
				msg.DescricaoMensagem = ex.Message;
				perfil.Mensagens.Add(msg);
				return false;
			}
		}

		public List<A6Perfil> RetornarPerfisUsuarioExterno(A1Usuario usuarioExterno)
		{
			List<A6Perfil> perfis = new List<A6Perfil>();
			foreach (var item in usuarioExterno.RelUsuarioPerfil)
			{
				perfis.Add(item.A6Perfil);
			}
			return perfis;
		}

		public List<A6Perfil> RetornarPerfisUsuarioInterno(A2UsuarioInterno usuarioInterno)
		{
			List<A6Perfil> perfis = new List<A6Perfil>();
			foreach (var item in usuarioInterno.RelUsuarioInternoPerfil)
			{
				perfis.Add(item.A6Perfil);
			}
			return perfis;
		}
	}
}
