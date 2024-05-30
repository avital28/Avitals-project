using Avitals_project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;


namespace Avitals_project.Handlers
{
    public class CustomDictionary:Dictionary<GenericParameter, GenericParameter>
    {
        public Dictionary<string, object> dict = new Dictionary<string, object>();
        public CustomDictionary(Dictionary<string, List<object>> d, ObservableCollection<Album> al) 
        { 
            d= dict.GetValueOrDefault(dict.ElementAt(0).Key) as Dictionary<string, List<object>>;   
            if (d != null )
            {
                foreach (object item in d.Values) 
                { 
                    if (item is Album)
                    {
                        al.Add(item as Album);
                    }
                }
            }
        }
    }
}
