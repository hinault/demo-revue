using Etudiants.MVC.Interfaces;
using Etudiants.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Etudiants.MVC.Controllers
{
    [Authorize]
    public class EtudiantsController : Controller
    {
        private readonly IDataProtector _protector;
        private readonly IEtudiantsService _etudiantsproxy;
        // GET: EtudiantsController

        public EtudiantsController(IEtudiantsService etudiantsproxy, IDataProtectionProvider protectionProvider)
        {
            _etudiantsproxy = etudiantsproxy;
            _protector = protectionProvider.CreateProtector("nas");
        }

        
        public async Task<ActionResult> Index()
        {
            //Revue de code 
            var etudiants = (await _etudiantsproxy.ObtenirTout()).ToList();
         
            etudiants.ForEach(e => e.NAS = _protector.Unprotect(e.NAS));

            return View(etudiants);
        }


        [Authorize(Roles ="Administrateur")]
        // GET: EtudiantsController/Create
        public ActionResult Create()
        {
            
            return View();
        }

        [Authorize(Roles = "Administrateur")]
        // POST: EtudiantsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Etudiant etudiant)
        {
            if(ModelState.IsValid)
            {
                etudiant.NAS = _protector.Protect(etudiant.NAS);
                _etudiantsproxy.Ajouter(etudiant);
                return RedirectToAction("Index");
            }

          return View(etudiant);
           
        }

       
    }
}
