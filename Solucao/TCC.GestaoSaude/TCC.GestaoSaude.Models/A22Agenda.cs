﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCC.GestaoSaude.Models
{
    [Table("A22_Agenda")]
    public partial class A22Agenda
    {
        [Key]
        [Column("A22_Agenda_ID")]
        public int A22AgendaId { get; set; }
        [Required]
        [Column("A12_Ocupacao_Codigo")]
        [StringLength(250)]
        public string A12OcupacaoCodigo { get; set; }
        [Column("A23_TipoAgenda_ID")]
        public int A23TipoAgendaId { get; set; }
        [Column("A22_Agenda_Data", TypeName = "date")]
        public DateTime? A22AgendaData { get; set; }
        [Column("A22_Agenda_Hora", TypeName = "time(0)")]
        public TimeSpan? A22AgendaHora { get; set; }
        [Column("A22_Agenda_DataCancelamento", TypeName = "date")]
        public DateTime? A22AgendaDataCancelamento { get; set; }
        [Column("A22_Agenda_Hora_2", TypeName = "time(0)")]
        public TimeSpan? A22AgendaHora2 { get; set; }
        [Column("A22_Agenda_UsuarioInternoExterno")]
        public int? A22AgendaUsuarioInternoExterno { get; set; }

        [ForeignKey(nameof(A12OcupacaoCodigo))]
        [InverseProperty(nameof(A12Ocupacao.A22Agenda))]
        public virtual A12Ocupacao A12OcupacaoCodigoNavigation { get; set; }
        [ForeignKey(nameof(A23TipoAgendaId))]
        [InverseProperty("A22Agenda")]
        public virtual A23TipoAgenda A23TipoAgenda { get; set; }
    }
}