using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ouvidoria_Projeto.Models
{
    public class Solicitante
    {
        [Key]
        public int SolicitanteId { get; set; }
        
        [DataType(DataType.Text)]
        public String Nome { get; set; }
        
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "O {0} deve ser menor que {2} e no maxímo {1} caractere.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Display(Name="Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento;

        public int Status { get; set; }
        
        [Display(Name ="Telefone")]
        [Phone(ErrorMessage ="Numero inválido!")]
        [Required]
        public string Telefone { get; set; }


        [Display(Name = "Nome")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }

    }
}
