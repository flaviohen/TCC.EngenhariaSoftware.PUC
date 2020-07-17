﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCC.GestaoSaude.Models
{
    [Table("Rel_UsuarioInternoPerfil")]
    public partial class RelUsuarioInternoPerfil
    {
        [Key]
        [Column("A2_UsuarioInterno_ID")]
        public int A2UsuarioInternoId { get; set; }
        [Key]
        [Column("A6_Perfil_ID")]
        public int A6PerfilId { get; set; }

        [ForeignKey(nameof(A2UsuarioInternoId))]
        [InverseProperty("RelUsuarioInternoPerfil")]
        public virtual A2UsuarioInterno A2UsuarioInterno { get; set; }
        [ForeignKey(nameof(A6PerfilId))]
        [InverseProperty("RelUsuarioInternoPerfil")]
        public virtual A6Perfil A6Perfil { get; set; }
    }
}