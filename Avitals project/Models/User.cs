using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Avitals_project.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }  
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateOnly Birthday { get; set; }



    }
}
