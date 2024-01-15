using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avitals_project.Models
{
    public class Album
    {
        public string AlbumCover { get; set; }
        public List<string> Media { get; set; }
        public List <User> Members { get; set; }    

        public string Location { get; set; }        
    }
}
