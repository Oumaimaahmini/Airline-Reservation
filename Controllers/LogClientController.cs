using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteVol.Models;

namespace SiteVol.Controllers
{
    public class LogClientController : Controller
    {
        String CS = "Data Source =. ; database=volDB; Integrated Security = SSPI";
        SqlDataReader dr;
        List<Reservation> res = new List<Reservation>();
        List<VolComp> vol = new List<VolComp>();

        // GET: LogClient

        [HttpGet]
        [Authorize]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        
        public ActionResult Booking( string id_vol, string destination, string date_depart, string prix, string compagnie)
        {
            
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO reservation ([id_vol],[destination],[date_depart],[prix],[compagnie]) VALUES ('" + id_vol + "','" + destination + "','" + date_depart + "','" + prix + "','" + compagnie + "')", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return View("Booking");
        }
        [HttpPost]
        public ActionResult Verify(LogClient log)
        {

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from Client where username = '" + log.username + "' and password = '" + log.password + "' ", con);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    log.clientName = log.username;


                    con.Close();
                    return View("ClientMenu", log);
                }
                else
                {
                    con.Close();
                    return View("Error");
                }
            }

        }
        [HttpPost]
        public ActionResult Reserver(Reservation reserv, LogClient logi)
        {
            

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("UPDATE reservation SET [username]='" + reserv.username + "',[CIN]='"+ reserv.CIN+ "',[nom]='" + reserv.nom + "',[prenom]='" + reserv.prenom + "',[nb_place]='"+ reserv.nb_place + "' WHERE ID = (SELECT max(ID) FROM reservation)", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                logi.clientName = reserv.username;

                return View("ClientMenu",logi);
              
            }
           
        }
        public void FeachData(string name)
        {
            if (res.Count > 0)
            {
                res.Clear();
            }
            try
            {
                SqlConnection con = new SqlConnection(CS);
                SqlCommand cmd = new SqlCommand("SELECT TOP 1000 [ID],[id_vol],[username],[CIN],[nom],[prenom],[destination],[date_depart],[prix],[nb_place],[compagnie] FROM[volDB].[dbo].[reservation] WHERE username='"+name+"'", con);
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    res.Add(new Reservation()
                    {
                        ID = dr["ID"].ToString(),
                        id_vol = dr["id_vol"].ToString(),
                        username = dr["username"].ToString(),
                        CIN = dr["CIN"].ToString(),
                        nom = dr["nom"].ToString(),
                        prenom = dr["prenom"].ToString(),
                        destination = dr["destination"].ToString(),
                        date_depart = dr["date_depart"].ToString(),
                        prix = dr["prix"].ToString(),
                        nb_place = dr["nb_place"].ToString(),
                        compagnie = dr["compagnie"].ToString()
                    });
                }
                con.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public ActionResult AnnulerReservation(string name)

        {
            FeachData(name);
            return View(res);
        }
        public ActionResult Delete(int ID, string name, LogClient logi)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM reservation WHERE ID = '"+ID+"'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                
                logi.clientName = name;


            }

            return View("ClientMenu",logi);
        }
        public ActionResult Recherche()
        {
            return View();
        }
        public void FeachDataRecherche(VolComp voli)
        {
            if (vol.Count > 0)
            {
                vol.Clear();
            }
            try
            {
                SqlConnection con = new SqlConnection(CS);
                SqlCommand cmd = new SqlCommand("SELECT TOP 1000 [id],[nombre_max],[destination],[ville_depart],[date_depart],[prix],[compagnie] FROM [volDB].[dbo].[Vol] where destination='" + voli.destination + "' and ville_depart='"+voli.ville_depart+"' ", con);
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    vol.Add(new VolComp()
                    {
                        id = dr["id"].ToString(),
                        nombre_max = dr["nombre_max"].ToString(),
                        destination = dr["destination"].ToString(),
                        ville_depart = dr["ville_depart"].ToString(),
                        date_depart = dr["date_depart"].ToString(),
                        prix = dr["prix"].ToString(),
                        compagnie = dr["compagnie"].ToString()
                    });
                }
                con.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        public ActionResult Rechercher(VolComp voli)
        {
            FeachDataRecherche(voli);
            return View("Resultat",vol);
        }
    }
}