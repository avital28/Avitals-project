using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avitals_project.Models
{
    public class UpdateUserDto:User
    {
        public UpdateUserDto() 
        {
            Firstname = null;
            Lastname = null;
            Username = null;
            Passwrd = null;
        }  
    }
}
