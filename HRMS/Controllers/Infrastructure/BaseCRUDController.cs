using Hrms.BusinessEntities;
using Hrms.Models;
using Hrms.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

[assembly: CLSCompliant(true)]
namespace Hrms.Controllers
{
    public abstract class BaseCRUDController<T> : Controller
        where T : class, new()
    {
        private IBaseRepository<T> repo = null;
        protected ModelContainer<T> Container { get; private set; }
        protected BaseCRUDController(string entityName, IBaseRepository<T> repo)
        {
            this.repo = repo;
            this.Container = new ModelContainer<T>(entityName);
        }
        //
        // GET: /Default1/

        public ActionResult Index()
        {
            //Index Page lists the available entities
            return NavigetToBodyLayout(Mode.List, repo.List.ToList());
        }

        //
        // GET: /Default1/Details/5

        public ActionResult Details(int id)
        {
            T objInstance = repo.SingleOrDefault(id);

            if (objInstance == null)
            {
                return HttpNotFound();
            }

            return NavigetToBodyLayout(Mode.Detail, repo.List.ToList(), objInstance);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            return NavigetToBodyLayout(Mode.Create, repo.List.ToList());
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        public ActionResult Create(T objInstance)
        {
            if (ModelState.IsValid)
            {
                repo.Insert(objInstance);
                return RedirectToAction("Index");
            }

            return NavigetToBodyLayout(Mode.Create, repo.List.ToList(), objInstance);
        }

        //
        // GET: /Default1/Edit/5

        public ActionResult Edit(int id)
        {
            T objInstance = repo.SingleOrDefault(id);

            if (objInstance == null)
            {
                return HttpNotFound();
            }

            return NavigetToBodyLayout(Mode.Edit, repo.List.ToList(), objInstance);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(T objInstance)
        {
            if (ModelState.IsValid)
            {
                repo.Update(objInstance);
                return RedirectToAction("Index");
            }

            return NavigetToBodyLayout(Mode.Edit, repo.List.ToList(), objInstance);
        }

        //
        // GET: /Default1/Delete/5

        public ActionResult Delete(int id)
        {
            T objInstance = repo.SingleOrDefault(id);
            if (objInstance == null)
            {
                return HttpNotFound();
            }
            return View(objInstance);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            T objInstance = repo.SingleOrDefault(id);

            repo.Delete(objInstance);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            //uow.Dispose();
            base.Dispose(disposing);
        }

        protected abstract void PopulateDomainValueDictionary();

        private ActionResult NavigetToBodyLayout(Mode Mode, List<T> ListOfItems, T Instance = default(T))
        {
            Container.SetValues(Mode, ListOfItems, Instance);

            if (Container.Mode == Models.Mode.Edit || Container.Mode == Models.Mode.Create) 
            { 
                PopulateDomainValueDictionary();
            }
            
            return View("_BodyLayout", Container);
        }
	}
}