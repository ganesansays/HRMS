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
    public abstract class BaseController<T> : Controller
        where T : class, new()
    {
        protected IUnitOfWork uow {get; private set;}
        private BaseRepository<T> repo = null;
        private string EntityName;
        protected ModelContainer<T> Container { get; private set; }
        public BaseController(string EntityName)
        {
            uow = new UnitOfWork();
            repo = new BaseRepository<T>(uow);
            this.EntityName = EntityName;
            this.Container = new ModelContainer<T>(EntityName);
        }
        //
        // GET: /Default1/

        public ActionResult Index()
        {
            Container.SetValues(Mode.LIST, repo.GetAll().ToList());
            PopulateDomainValueDictionary();
            return View("_BodyLayout", Container);
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
            return View(objInstance);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            Container.SetValues(Mode.CREATE, repo.GetAll().ToList());
            PopulateDomainValueDictionary();
            return View("_BodyLayout", Container);
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

            Container.SetValues(Mode.CREATE, repo.GetAll().ToList(), objInstance);
            PopulateDomainValueDictionary();
            return View("_BodyLayout", Container);
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

            Container.SetValues(Mode.EDIT, repo.GetAll().ToList(), objInstance);
            PopulateDomainValueDictionary();
            return View("_BodyLayout", Container);
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

            Container.SetValues(Mode.EDIT, repo.GetAll().ToList(), objInstance);
            PopulateDomainValueDictionary();
            return View("_BodyLayout", Container);
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

        protected void Add(string key, IEnumerable<SelectListItem> value)
        {
            Container.DomainValueDictionary.Add(key, value);
        }
	}
}