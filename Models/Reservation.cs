using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteVol.Models
{
    public class Reservation
    {
        public string ID { get; set; }
        public string username { get; set; }
        public string CIN { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string nb_place { get; set; }
        public string id_vol { get; set; }
        public string destination { get; set; }
        public string date_depart { get; set; }
        public string prix { get; set; }
        public string compagnie { get; set; }
    }
}