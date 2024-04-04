using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avitals_project.Models
{
    public class MediaItem
    {
        public string Sources { get; set; }  
        public bool IsImage { get; set; }   
        public bool IsVideo { get; set; }

        public MediaItem()
        {
            string str = Sources.Substring(Sources.Length - 3);
            if (str=="jpg" || str=="png"|| str=="svg")
            {
                IsImage = true;
                IsVideo = false;
            }
            else
            {
                IsImage = false;
                IsVideo= true;  
            }
        }
    }
}
