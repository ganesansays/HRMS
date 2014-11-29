using Hrms.BusinessEntities;
using Hrms.Controllers;
using Hrms.Models;
using Hrms.Repository;
using HRMS.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HRMS.Tests.Controllers
{
    enum ControllerAction
    {
        Create,
        Edit
    }

    public class ErrorScenarioContext<EntityType>
    {
        public string Name { get; private set; }
        public EntityType ErrorEntity { get; private set; }
        public Dictionary<string, string> Errors { get; private set; }

        public ErrorScenarioContext(string name, EntityType errorEntity, Dictionary<string, string> errors)
        {
            Name = name;
            ErrorEntity = errorEntity;
            Errors = errors;
        }
    }

    [TestClass]
    public abstract class BaseCRUDControllerTest<ControllerType, EntityType>
        where ControllerType : BaseCRUDController<EntityType>
        where EntityType : EntityBase<EntityType>, new()
    {
        protected ControllerType Controller { get; private set; }

        protected IRepositoryContext RepoContext { get; private set; }

        private string entityName;

        private List<ErrorScenarioContext<EntityType>> errorScenarios;

        protected void SetErrorScenarios(List<ErrorScenarioContext<EntityType>> errorScenarios)
        {
            this.errorScenarios = errorScenarios;
        }

        public void ClearValidationResults(bool dbDrop = false)
        {
            RepoContext = new DBRepositoryContext(new UnitOfWork(new HrmsStore(dbDrop)));
            Controller = Activator.CreateInstance(typeof(ControllerType),
                  new object[] { RepoContext }) as ControllerType;
        }

        protected BaseCRUDControllerTest(string entityName) 
        {
            ClearValidationResults(true);
            this.entityName = entityName;
        }

        [TestMethod]
        public void TestCreatePage()
        {
            //Act
            ActionResult result = Controller.Create();
            ViewResult vResult = result as ViewResult;

            //Assert
            Assert.IsInstanceOfType(vResult.Model, typeof(ModelContainer<EntityType>), "Page is not redirected to create page");
            Assert.AreEqual((vResult.Model as ModelContainer<EntityType>).Instance.Id, 0, "Entity viewed in create page is not a new " + entityName);
            Assert.AreEqual((vResult.Model as ModelContainer<EntityType>).Name, entityName, "Heading shown is not \"" + entityName + "\"");
        }

        internal void TestForErrors(ErrorScenarioContext<EntityType> scenario, ControllerAction action)
        {
            EntityType errorEntity = scenario.ErrorEntity;
            Dictionary<string, string> errors = scenario.Errors;

            DbEntityValidationResult validationResult = null;
            DbEntityValidationException validationException = null;
            try
            {
                //Act
                ActionResult result = null;
                switch(action)
                {
                    case ControllerAction.Create:
                        result = Controller.Create(errorEntity);
                        break;
                    case ControllerAction.Edit:
                        EntityType entity = GetDummyEntity();
                        int addedEntityId = RepoContext.GetRepository<EntityType>().Insert(entity);
                        Assert.AreNotEqual(0, addedEntityId, "Entity not inserted to perform update.");
                        
                        EntityType entityToBeUpdated = RepoContext.GetRepository<EntityType>().SingleItem(addedEntityId);
                        entityToBeUpdated.Merge(errorEntity);
                        result = Controller.Edit(entityToBeUpdated);
                        break;
                    default:
                        return;
                }
            }
            catch (DbEntityValidationException ex)
            {
                validationException = ex;

                //Assert
                Assert.IsNotNull(ex.EntityValidationErrors);
                IEnumerable<DbEntityValidationResult> EntityValidationErrors = ex.EntityValidationErrors;

                validationResult = EntityValidationErrors.FirstOrDefault();
                Assert.IsNotNull(validationResult);
                Assert.IsNotNull(validationResult.ValidationErrors);

                Assert.AreEqual(errors.Count, validationResult.ValidationErrors.Count, scenario.Name + " : " + action + " : Number of validation errors is not matching.");

                ClearValidationResults();
            }

            if (errors != null && errors.Count > 0)
            {
                DbValidationErrorComparer comparer = new DbValidationErrorComparer();
                foreach (KeyValuePair<string, string> entry in errors)
                {
                    string error = scenario.Name + " : " + action + " : \"" + entry.Key + " - " + entry.Value + "\" : error is not thrown";

                    Assert.IsFalse(validationResult == null || validationResult.ValidationErrors.Count == 0, error);

                    Assert.AreEqual(
                        validationResult.ValidationErrors.Contains(
                            new DbValidationError(entry.Key, entry.Value),
                            comparer),
                        true, error);
                }
            }
        }

        [TestMethod]
        public void TestErrorScenarios()
        {
            if (errorScenarios != null && errorScenarios.Count > 0)
            {
                foreach (ErrorScenarioContext<EntityType> scenario in errorScenarios)
                {
                    TestForErrors(scenario, ControllerAction.Create);
                    TestForErrors(scenario, ControllerAction.Edit);
                }
            }
        }

        [TestMethod]
        public void TestCreateAnEntity()
        {
            EntityType dummyEntity = GetDummyEntity();
            EntityType createdEntity = TestCreateAnEntity(dummyEntity);

            Assert.IsNotNull(createdEntity);

            Console.WriteLine(dummyEntity.Id);
            Assert.IsTrue(dummyEntity.CompareByValue(createdEntity), "Created " + entityName + " is not equal to dummy " + entityName);
        }

        internal EntityType TestCreateAnEntity(EntityType entity)
        {
            //Arrange
            List<EntityType> entityListBeforeAdd = RepoContext.GetRepository<EntityType>().List.ToList();
            int countBeforeAdd = entityListBeforeAdd.Count;

            //Act
            ActionResult result = Controller.Create(entity);

            //Assert
            RedirectToRouteResult routeResult = result as RedirectToRouteResult;
            Assert.AreEqual(routeResult.RouteValues["action"], "Index");

            Assert.IsNotNull(RepoContext.GetRepository<EntityType>().List);
            List<EntityType> entityListAfterAdd = RepoContext.GetRepository<EntityType>().List.ToList();
            Assert.AreEqual(entityListAfterAdd.Count, countBeforeAdd + 1);
            Assert.AreNotEqual(entityListAfterAdd[countBeforeAdd].Id, 0);

            return entityListAfterAdd[countBeforeAdd];
        }

        [TestMethod]
        public void TestEditAnEntity()
        {
            //Step 1. Added a dummy group and view it for editing
            //Arrange
            EntityType dummyEnity = GetDummyEntity();
            //Act
            EntityType entityToBeEdited = TestEditPageView(dummyEnity);
            //Assert
            Assert.IsTrue(dummyEnity.CompareByValue(entityToBeEdited));

            //Step 2: Update some information of the dummy group and test
            //Arrange
            EntityType entityToBeSaved = GetEntityToBeSaved(entityToBeEdited);
            //Act
            EntityType editedEntity = TestEditPost(entityToBeSaved);
            //Assert
            Assert.IsTrue(editedEntity.CompareByValue(entityToBeSaved));
        }

        protected abstract EntityType GetEntityToBeSaved(EntityType inputEntity);

        internal EntityType TestEditPageView(EntityType entity)
        {
            //Arrange
            int addedEntityId = RepoContext.GetRepository<EntityType>().Insert(entity);

            //Act
            ActionResult result = Controller.Edit(addedEntityId);
            ViewResult vResult = result as ViewResult;

            //Assert
            Assert.IsInstanceOfType(vResult.Model, typeof(ModelContainer<EntityType>));
            Assert.AreEqual((vResult.Model as ModelContainer<EntityType>).Instance.Id, addedEntityId);

            return RepoContext.GetRepository<EntityType>().List.Where(e => e.Id == addedEntityId).FirstOrDefault();
        }

        [TestMethod]
        public void TestDeleteAnEntity()
        {
            EntityType dummyEntity = GetDummyEntity();
            TestDeletePost(dummyEntity);
        }

        protected abstract EntityType GetDummyEntity();


        internal EntityType TestEditPost(EntityType entityToBeEdited)
        {
            ActionResult result = Controller.Edit(entityToBeEdited);
            EntityType editedEntity = RepoContext.GetRepository<EntityType>().SingleItem(entityToBeEdited.Id);

            //Assert
            RedirectToRouteResult routeResult = result as RedirectToRouteResult;
            Assert.AreEqual(routeResult.RouteValues["action"], "Index");

            return editedEntity;
        }

        internal void TestDeletePost(EntityType dummyEntity)
        {
            IEnumerable<EntityType> initialEntities = RepoContext.GetRepository<EntityType>().List;
            int initialCount = 0;
            if (initialEntities != null) initialCount = initialEntities.ToList().Count;

            int dummyEntityId = RepoContext.GetRepository<EntityType>().Insert(dummyEntity);

            ActionResult result = Controller.DeleteConfirmed(dummyEntityId);
            IEnumerable<EntityType> finalEntities = RepoContext.GetRepository<EntityType>().List;
            int finalCount = 0;
            if (finalEntities != null) finalCount = finalEntities.ToList().Count;

            RedirectToRouteResult routeResult = result as RedirectToRouteResult;
            Assert.AreEqual(routeResult.RouteValues["action"], "Index");
            Assert.AreEqual(initialCount, finalCount);
        }

        internal int CreateDummyEntry<DummyEntityType>(DummyEntityType dummy)
            where DummyEntityType : EntityBase<DummyEntityType>, new()
        {
            return RepoContext.GetRepository<DummyEntityType>().Insert(dummy);
        }
    }
}
