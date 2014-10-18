using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hrms.Controllers;
using Hrms.BusinessEntities;

namespace HRMS.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest : BaseCRUDControllerTest<UserController, User>
    {
        public UserControllerTest()
            : base("User")
        {

        }

        [TestMethod]
        public void TestCreateAUser()
        {
            int addedGroupId = CreateDummyEntry(Group.Sample());

            User user = User.Sample();
            user.Name = "User Name";
            user.Password = "Password";
            user.GroupId = addedGroupId;

            User addedUser = TestCreateAnEntity(user);
        }

        [TestMethod]
        public void TestEditAUser()
        {
            //Step 1. Added a dummy group and view it for editing
            //Arrange
            int addedGroupId = CreateDummyEntry(Group.Sample());
            User dummyUser = User.Sample();
            dummyUser.GroupId = addedGroupId;

            //Act
            User userToBeEdited = TestEditPageView(dummyUser);
            //Assert
            Assert.AreEqual(userToBeEdited.Name, dummyUser.Name);

            //Step 2: Update some information of the dummy group and test
            //Arrange
            userToBeEdited.Name = "Updated User Name";
            //Act
            User editedUser = TestEditPost(userToBeEdited);
            //Assert
            Assert.AreEqual(editedUser.Name, "Updated User Name");
        }

        [TestMethod]
        public void TestDeleteAUser()
        {
            int addedGroupId = CreateDummyEntry(Group.Sample());
            User dummyUser = User.Sample();
            dummyUser.GroupId = addedGroupId;

            TestDeletePost(dummyUser);
        }
    }
}
