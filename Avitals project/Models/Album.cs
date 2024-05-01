using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Avitals_project.Models
{
    public class Album
    {
        public string AlbumCover { get; set; }
        [JsonPropertyName("Media")]
        public List<Media> Media { get; set; }
        public bool IsPublic { get; set; }  
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int AdminId { get; set; } 
        public string AlbumTitle { get; set; }  
        public int Id { get; set; }
        public List<User> Memebers { get; set; }
        

        public Album()
        {
            Media = new List<Media>();
            Memebers = new List<User>();
        
        }
    }
}
