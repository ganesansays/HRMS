using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hrms.Controllers;
using Hrms.BusinessEntities;
using System.Collections.Generic;

namespace HRMS.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest : BaseCRUDControllerTest<UserController, User>
    {
        public UserControllerTest()
            : base("User")
        {
            List<ErrorScenarioContext<User>> errorScenarios = new List<ErrorScenarioContext<User>>();
            errorScenarios.Add(GetRequiredFieldsErrorScenario());
            errorScenarios.Add(GetMaxLengthErrorScenario());
            errorScenarios.Add(GetMinLengthErrorScenario());

            SetErrorScenarios(errorScenarios);
        }

        private ErrorScenarioContext<User> GetMaxLengthErrorScenario()
        {
            User entity = new User();
            entity.PopulateDummyValues();

            Group dummyGroup = new Group();
            dummyGroup.PopulateDummyValues();
            entity.GroupId = CreateDummyEntry(dummyGroup);

            entity.Password = "1234567890123456789012345678901";
            Dictionary<string, string> requiredFields = new Dictionary<string, string>();

            requiredFields.Add("Password", "Must be between 6 and 30 characters long.");

            return new ErrorScenarioContext<User>("MaxLength", entity, requiredFields);
        }

        private ErrorScenarioContext<User> GetMinLengthErrorScenario()
        {
            User entity = new User();
            entity.PopulateDummyValues();

            Group dummyGroup = new Group();
            dummyGroup.PopulateDummyValues();
            entity.GroupId = CreateDummyEntry(dummyGroup);

            entity.Password = "1234";
            Dictionary<string, string> requiredFields = new Dictionary<string, string>();

            requiredFields.Add("Password", "Must be between 6 and 30 characters long.");

            return new ErrorScenarioContext<User>("MaxLength", entity, requiredFields);
        }

        public ErrorScenarioContext<User> GetRequiredFieldsErrorScenario()
        {
            User entity = new User();
            Dictionary<string, string> requiredFields = new Dictionary<string, string>();

            requiredFields.Add("Name", "User Name Cannot be blank.");
            requiredFields.Add("Password", "Password cannot be blank.");
            requiredFields.Add("GroupId", "Select a group for this user.");

            return new ErrorScenarioContext<User>("RequiredFields", entity, requiredFields);
        }

        protected override User GetDummyEntity()
        {
            Group dummyGroup = new Group();
            dummyGroup.PopulateDummyValues();
            int addedGroupId = CreateDummyEntry(dummyGroup);
            User dummyUser = new User();
            dummyUser.PopulateDummyValues();
            dummyUser.GroupId = addedGroupId;

            return dummyUser;
        }

        protected override User GetEntityToBeSaved(User inputEntity)
        {
            inputEntity.Name = "Updated User Name";
            inputEntity.Password = "NewPassword";

            Group dummyGroup = new Group();
            dummyGroup.PopulateDummyValues();

            int addedGroupId = CreateDummyEntry(dummyGroup);
            inputEntity.GroupId = addedGroupId;

            return inputEntity;
        }
    }
}
