using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avitals_project.Models
{
    public class UserDto:User
    {
        public User User {  get; set; } 
        public string Message {  get; set; }
    }
}
