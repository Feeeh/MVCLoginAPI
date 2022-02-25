using System;

namespace MVCLoginAPI2.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public DateTime DataDeCriacao { get; set; }
        public bool Termos { get; set; }
    }
}
