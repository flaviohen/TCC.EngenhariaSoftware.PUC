﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCC.GestaoSaude.Models
{
    [Table("Rel_HistoricoEvolucaoMedicaDiaria")]
    public partial class RelHistoricoEvolucaoMedicaDiaria
    {
        [Key]
        [Column("A9_Prontuario_ID")]
        public int A9ProntuarioId { get; set; }
        [Key]
        [Column("A11_RegistroEvolucaoMedicaDiaria_ID")]
        public int A11RegistroEvolucaoMedicaDiariaId { get; set; }

        [ForeignKey(nameof(A11RegistroEvolucaoMedicaDiariaId))]
        [InverseProperty("RelHistoricoEvolucaoMedicaDiaria")]
        public virtual A11RegistroEvolucaoMedicaDiaria A11RegistroEvolucaoMedicaDiaria { get; set; }
        [ForeignKey(nameof(A9ProntuarioId))]
        [InverseProperty("RelHistoricoEvolucaoMedicaDiaria")]
        public virtual A9Prontuario A9Prontuario { get; set; }
    }
}