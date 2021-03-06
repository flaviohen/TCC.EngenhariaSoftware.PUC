﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TCC.GestaoSaude.Common;

namespace TCC.GestaoSaude.Models
{
    [Table("A6_Perfil")]
    public partial class A6Perfil
    {
        public A6Perfil()
        {
            RelUsuarioInternoPerfil = new HashSet<RelUsuarioInternoPerfil>();
            RelUsuarioPerfil = new HashSet<RelUsuarioPerfil>();
            Mensagens = new List<Mensagem>();
        }

        [Key]
        [Column("A6_Perfil_ID")]
        public int A6PerfilId { get; set; }
        [Column("A6_Perfil_Descricao")]
        [StringLength(100)]
        public string A6PerfilDescricao { get; set; }

        [InverseProperty("A6Perfil")]
        public virtual ICollection<RelUsuarioInternoPerfil> RelUsuarioInternoPerfil { get; set; }
        [InverseProperty("A6Perfil")]
        public virtual ICollection<RelUsuarioPerfil> RelUsuarioPerfil { get; set; }

        [NotMapped]
        public List<Mensagem> Mensagens { get; set; }
    }
}