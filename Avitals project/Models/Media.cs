using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Avitals_project.Models
{
    public class Media

    {
       
        public string Sources { get; set; }
        
        public bool IsImage { get; set; }
        public bool IsVideo { get; set; }

        public Media(string source)
        {
            Sources = source;
            MediaTypeCheck(source);
        }

        public void MediaTypeCheck(string src)
        {
            
            string str = src.Substring(src.Length - 3);
            if (str == "jpg" || str == "png" || str == "svg")
            {
                IsImage = true;
                IsVideo = false;
            }
            else
            {
                IsImage = false;
                IsVideo = true;
            }
        }
        public Media() { }
    }
}
