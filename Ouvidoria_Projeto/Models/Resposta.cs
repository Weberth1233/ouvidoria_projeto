using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ouvidoria_Projeto.Models
{
    public class Resposta
    {
        [Key]
        public int RespostaId { get; set; }
        [Required(ErrorMessage ="Campos resposta não informado !")]
        public string RespostaManifesto { get; set; }
        [Required]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O campo é requirido!")]
        [Display(Name = "Usuario")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo é requirido!")]
        [Display(Name = "Manifesto")]
        public int ManifestoId { get; set; }
       
        public virtual Manifesto Manifesto { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
    }
}
