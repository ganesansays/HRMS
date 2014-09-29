using HRMS.BusinessEntities;
using HRMS.Models;
using Repostitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRMS.Controllers
{
    public abstract class BaseCRUDController<T> : Controller
        where T : class, new()
    {
        protected IUnitOfWork uow {get; private set;}
        private BaseRepository<T> repo = null;
        protected ModelContainer<T> Container { get; private set; }
        public BaseCRUDController(string EntityName)
        {
            uow = new UnitOfWork();
            repo = new BaseRepository<T>(uow);
            this.Container = new ModelContainer<T>(EntityName);
        }
        //
        // GET: /Default1/

        public ActionResult Index()
        {
            //Index Page lists the available entities
            return NavigetToBodyLayout(Mode.LIST, repo.GetAll().ToList());
        }

        //
        // GET: /Default1/Details/5

        public ActionResult Details(int id = 0)
        {
            T objInstance = repo.SingleOrDefault(id);

            if (objInstance == null)
            {
                return HttpNotFound();
            }

            return NavigetToBodyLayout(Mode.DETAIL, repo.GetAll().ToList(), objInstance);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            return NavigetToBodyLayout(Mode.CREATE, repo.GetAll().ToList());
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

            return NavigetToBodyLayout(Mode.CREATE, repo.GetAll().ToList(), objInstance);
        }

        //
        // GET: /Default1/Edit/5

        public ActionResult Edit(int id = 0)
        {
            T objInstance = repo.SingleOrDefault(id);

            if (objInstance == null)
            {
                return HttpNotFound();
            }

            return NavigetToBodyLayout(Mode.EDIT, repo.GetAll().ToList(), objInstance);
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

            return NavigetToBodyLayout(Mode.EDIT, repo.GetAll().ToList(), objInstance);
        }

        //
        // GET: /Default1/Delete/5

        public ActionResult Delete(int id = 0)
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
            uow.Dispose();
            base.Dispose(disposing);
        }

        public virtual void PopulateDomainValueDictionary()
        {
            //Default Implementation
        }
        private ActionResult NavigetToBodyLayout(Mode Mode, List<T> ListOfItems, T Instance = default(T))
        {
            Container.SetValues(Mode, ListOfItems, Instance);

            if (Container.Mode == Models.Mode.EDIT || Container.Mode == Models.Mode.CREATE) 
            { 
                PopulateDomainValueDictionary();
            }
            
            return View("_BodyLayout", Container);
        }
	}
}