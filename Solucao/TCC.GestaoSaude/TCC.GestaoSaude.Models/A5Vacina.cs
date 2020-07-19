﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TCC.GestaoSaude.Common;

namespace TCC.GestaoSaude.Models
{
    [Table("A5_Vacina")]
    public partial class A5Vacina
    {
        [Key]
        [Column("A5_Vacina_ID")]
        public int A5VacinaId { get; set; }
        [Column("A4_CartaoVacinacao_ID")]
        public int A4CartaoVacinacaoId { get; set; }
        [Column("A5_Vacina_DataVacinacao", TypeName = "date")]
        public DateTime? A5VacinaDataVacinacao { get; set; }
        [Column("A5_Vacina_Descricao")]
        [StringLength(400)]
        public string A5VacinaDescricao { get; set; }

        [ForeignKey(nameof(A4CartaoVacinacaoId))]
        [InverseProperty("A5Vacina")]
        public virtual A4CartaoVacinacao A4CartaoVacinacao { get; set; }

        [NotMapped]
        public List<Mensagem> Mensagens { get; set; }
    }
}