using System;
using System.Collections.Generic;
using System.Text;

namespace CliMovApp.ec.edu.monster.modelo
{
    public class Usuario
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool EstaAutenticado { get; set; }
    }
}
