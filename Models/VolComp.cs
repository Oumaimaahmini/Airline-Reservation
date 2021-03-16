using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteVol.Models
{
    public class VolComp
    {
        public string id { get; set; }
        public string nombre_max { get; set; }
        public string destination { get; set; }
        public string ville_depart { get; set; }
        public string date_depart { get; set; }
        public string prix { get; set; }
        public string compagnie { get; set; }

    }
}