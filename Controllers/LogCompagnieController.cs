using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteVol.Models;

namespace SiteVol.Controllers
{
    
    public class LogCompagnieController : Controller
    {

        String CS = "Data Source =. ; database=volDB; Integrated Security = SSPI";
        SqlDataReader dr;
        
        List<VolComp> vol = new List<VolComp>();
        List<Place> place = new List<Place>();
        // GET: LogCompagnie
        [HttpGet]
        [Authorize]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Verify(LogCompagnie log)
        {
            
            using(SqlConnection con = new SqlConnection(CS)) 
            {
                SqlCommand cmd = new SqlCommand("select * from Compagnie where username = '" + log.name + "' and password = '" + log.password + "' ", con);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    log.CompanyName= log.name;
                    
                    
                    con.Close();
                    return View("CompangnieMenu", log);
                }
                else
                {
                    con.Close();
                    return View("Error");
                }
            }   

        }
        public void FeachData(String compa)
        {
            if (vol.Count>0)
            {
                vol.Clear();
            }
            try
            {
                SqlConnection con = new SqlConnection(CS);
                SqlCommand cmd = new SqlCommand("SELECT TOP 1000 [id],[nombre_max],[destination],[ville_depart],[date_depart],[prix],[compagnie] FROM [volDB].[dbo].[Vol] where compagnie='"+compa+"' ", con);
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
        
        public ActionResult AjouterSupprimerVols(String compa)
        {
            FeachData(compa);
            return View(vol);
        }
        public void FeachDataAutre(String name)
        {
            if (vol.Count > 0)
            {
                vol.Clear();
            }
            try
            {
                SqlConnection con = new SqlConnection(CS);
                SqlCommand cmd = new SqlCommand("SELECT TOP 1000 [id],[nombre_max],[destination],[ville_depart],[date_depart],[prix],[compagnie] FROM [volDB].[dbo].[Vol] where compagnie<>'" + name + "' ", con);
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

        public ActionResult AutreVols(String name)
        {
            FeachDataAutre(name);
            return View(vol);
        }

        public void FeachPlace()
        {
            if (place.Count > 0)
            {
                place.Clear();
            }
            try
            {
                SqlConnection con = new SqlConnection(CS);
                SqlCommand cmd = new SqlCommand("SELECT v.id,v.nombre_max,v.compagnie, SUM(r.nb_place) as reserve FROM Vol v,reservation r WHERE v.id=r.id_vol GROUP BY v.id,v.nombre_max,v.compagnie ORDER BY v.id  ", con);
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    place.Add(new Place()
                    {
                        id = dr["id"].ToString(),
                        nombre_max = dr["nombre_max"].ToString(),
                        compagnie = dr["compagnie"].ToString(),
                        reserve = dr["reserve"].ToString()
                    }); ;
                }
                con.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult AfficherPlace()
        {
            FeachPlace();
            return View(place);
        }
    }
}