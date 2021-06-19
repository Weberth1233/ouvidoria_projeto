using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ouvidoria_Projeto.Models
{
    public class Solicitante
    {
        [Key]
        public int SolicitanteId { get; set; }
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
        [DataType(DataType.DateTime)]
        public DateTime Data;

        public int Status { get; set; }
        public Sexo Sexo { get; set; }
        
        [Display(Name = "Usuario")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
    }
}

namespace Ouvidoria_Projeto
{
    public enum Sexo
    {
        Masculino=1,
        Feminino=2,
        Outro=3,
    }
}