using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.Business
{
	public class A1UsuarioBusiness
	{
		private readonly IA1UsuarioRepositorio _usuarioRepositorio;
		public A1UsuarioBusiness(IA1UsuarioRepositorio usuarioRepositorio)
		{
			_usuarioRepositorio = usuarioRepositorio;
		}
		public A1Usuario Logar(A1Usuario usuario) 
		{
			try
			{
				List<string> includes = new List<string>();
				includes.Add("RelUsuarioPerfil");
				var usuarioBuscado =  _usuarioRepositorio.FindAsync(c => c.A1UsuarioNumeroCpf == usuario.A1UsuarioNumeroCpf).Result;
				if (usuarioBuscado != null)
				{
					if (usuarioBuscado.A1UsuarioSenha.Equals(usuario.A1UsuarioSenha))
					{
						return usuarioBuscado;
					}
					else 
					{
						usuarioBuscado = null;
						return usuarioBuscado;
					}
				}
				else 
				{
					usuarioBuscado = null;
					return usuarioBuscado;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
