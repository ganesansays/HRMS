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
        : BaseCRUDControllerTest<GroupController, Group>
    {

        public GroupControllerTest()
            : base("Group")
        {

        }

        [TestMethod]
        public void TestCreateAGroup()
        {
            //Arrange
            Group groupToBeCreated = Group.Sample();
            
            //Act
            Group createdGroup = TestCreateAnEntity(groupToBeCreated);
            
            //Assert
            Assert.AreEqual(createdGroup.Name, groupToBeCreated.Name);
        }

        [TestMethod]
        public void TestEditAGroup()
        {
            //Step 1. Added a dummy group and view it for editing
            //Arrange
            Group dummyGroup = Group.Sample();
            //Act
            Group groupToBeEdited = TestEditPageView(dummyGroup);
            //Assert
            Assert.AreEqual(groupToBeEdited.Name, dummyGroup.Name);
            
            //Step 2: Update some information of the dummy group and test
            //Arrange
            groupToBeEdited.Name = "Updated Group Name";
            //Act
            Group editedGroup = TestEditPost(groupToBeEdited);
           //Assert
            Assert.AreEqual(editedGroup.Name, "Updated Group Name");
        }

        [TestMethod]
        public void TestDeleteAGroup()
        {
            Group dummyGroup = Group.Sample();

            TestDeletePost(dummyGroup);
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void TestMaxLengthForGroupName()
        {
            //Arrange
            Group group = Group.Sample();
            group.Name = "New Group 12345678901"; //21 characters, should throw error

            //Act
            TestCreateAnEntity(group);

            //Assert
            //No assertion required. Action should be throwing DbEntityValidationException
        }
    }
}
