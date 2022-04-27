using System.Text;

namespace RestWithASPNetUdemy.Hypermedia
{
    public class HypermediaLink
    {

        private string href;


        public string Rel { get; set; }

        public string Href {
            get 
            {
                object _lock = new object();
                lock (_lock) 
                { 
                    StringBuilder sb = new StringBuilder(href);
                    return sb.Replace("%2F", "/").ToString();   
                }                
            }
            set 
            { 
                href = value;
            } 
        }

        public string Type { get; set; }

        public string Action { get; set; }

    }
}
