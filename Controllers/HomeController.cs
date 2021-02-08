using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using aplicacionesdecontactos.Models;

namespace aplicacionesdecontactos.Controllers
{
    public class HomeController : Controller
    {
        contacto contactoss = new contacto();

        ContactosEntities db = new ContactosEntities();

        public ActionResult Home()
        {
           
                var lista = db.contactoes.ToList();
                return View(lista);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult crear(contacto cont)
        {
            try
            {
                if (cont.Nombre==null || cont.Telefono == null || cont.Direccion==null)
                {
                    ModelState.AddModelError("Todos los campos tienen que estar lleno","ok");
                }

                cont.estatus = "true";
                db.contactoes.Add(cont);
                db.SaveChanges();
                return RedirectToAction("Home");
            }
            catch (Exception x)
            {
                
                return View();
            }
            
        }

        public ActionResult crear()
        {

            return View();
        }
        [HttpGet]
        public ActionResult Editar(int id)
        {
            var edit = GetContacto(id);
            return View(edit);
        }

        [HttpPost]
        public ActionResult Editar(contacto cont)
        {
            try
            {
                var datos = db.contactoes.Find(cont.id);
                datos.Nombre = cont.Nombre;
                datos.Direccion = cont.Direccion;
                datos.Telefono = cont.Telefono;
                db.SaveChanges();
                return RedirectToAction("home");
            }
            catch (Exception x)
            {

                Console.WriteLine("Error", x.Message);
                return View("Editar");
            }
           

           
        }


        public ActionResult eliminar(int id)
        {

            var eli = db.contactoes.Find(id);
            db.contactoes.Remove(eli);

            return Content("1");
        }




        public contacto GetContacto(int id)
        {
            var datos =  db.contactoes.Where(d => d.id == id).FirstOrDefault();
            return datos;
        }






        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}