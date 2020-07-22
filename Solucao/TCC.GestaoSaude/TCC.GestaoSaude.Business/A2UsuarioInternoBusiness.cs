using System;
using System.Collections.Generic;
using System.Text;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.Business
{
	public class A2UsuarioInternoBusiness
	{
		private readonly IA2UsuarioInternoRepositorio _usuarioInternoRepositorio;

		public A2UsuarioInternoBusiness(IA2UsuarioInternoRepositorio usuarioInternoRepositorio) 
		{
			_usuarioInternoRepositorio = usuarioInternoRepositorio;
		}
		public bool CriarLoginInterno(A2UsuarioInterno usuario)
		{
			try
			{
				_usuarioInternoRepositorio.Add(usuario);
				_usuarioInternoRepositorio.Save();

				return usuario.A2UsuarioInternoId > 0;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
