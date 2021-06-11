using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ouvidoria_Projeto.Models
{
    public class Manifesto
    {
        [Key]
        public int ManifestoId { get; set; }

        [Required(ErrorMessage = "Campo título deve ser informado!")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Campo descrição deve ser informado!")]
        public string Descricao { get; set; }

        [Required]
        public int Status { get; set; }
        [Required(ErrorMessage = "Campo Data deve ser informado!")]
        public DateTime Data { get; set; }

        [Display(Name = "Tipo Manifesto")]
        [ForeignKey("TipoManifesto")]
        public int TipoManifestoId { get; set; }
        public virtual TipoManifesto TipoManifesto { get; set; }

        public virtual Resposta Resposta { get; set; }
    }
}