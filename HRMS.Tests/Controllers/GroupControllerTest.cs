using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hrms.BusinessEntities;
using Hrms.Repository;
using Hrms.Controllers;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using System.Collections.Generic;
using System.Linq;
using Hrms.Models;

namespace HRMS.Tests.Controllers
{
    [TestClass]
    public class GroupControllerTest
    {
        [TestMethod]
        public void CleanUp()
        {
            HrmsStore store = new HrmsStore();
            store.Database.ExecuteSqlCommand("delete from dbo.groups");
        }

        [TestMethod]
        public void TestCreatePage()
        {
            IRepositoryContext RepoContext = new DBRepositoryContext(new UnitOfWork(new HrmsStore()));
            GroupController controller = new GroupController(RepoContext);

            //Act
            ActionResult result = controller.Create();
            ViewResult vResult = result as ViewResult;

            //Assert
            Assert.IsInstanceOfType(vResult.Model, typeof(ModelContainer<Group>));
            Assert.AreEqual((vResult.Model as ModelContainer<Group>).Instance.Id, 0);
        }
        
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void TestCreateAGroupWithoutMandatoryValues()
        {
            //Arrange
            Group group = new Group();

            IRepositoryContext RepoContext = new DBRepositoryContext(new UnitOfWork(new HrmsStore()));
            GroupController controller = new GroupController(RepoContext);

            //Act
            ActionResult result = controller.Create(group);
        }

        [TestMethod]
        public void TestCreateAGroupWithMandatoryValues()
        {
            //Arrange
            Group group = new Group();
            group.Name = "Dummy Group Name";

            IRepositoryContext RepoContext = new DBRepositoryContext(new UnitOfWork(new HrmsStore()));
            GroupController controller = new GroupController(RepoContext);

            //Act
            ActionResult result = controller.Create(group);
            RedirectToRouteResult routeResult = result as RedirectToRouteResult;

            //Assert
            Assert.AreEqual(routeResult.RouteValues["action"], "Index");
            List<Group> groupList = RepoContext.GetRepository<Group>().List.ToList();
            Assert.AreEqual(groupList.Count, 1);
            Assert.AreEqual(groupList[0].Name, "Dummy Group Name");

            //Clean up
            CleanUp();
        }
    }
}
