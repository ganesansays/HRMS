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
            List<ErrorScenarioContext<Group>> errorScenarios = new List<ErrorScenarioContext<Group>>();
            errorScenarios.Add(GetRequiredFieldsErrorScenario());
            errorScenarios.Add(GetMaxLengthErrorScenario());

            SetErrorScenarios(errorScenarios);
        }

        private ErrorScenarioContext<Group> GetMaxLengthErrorScenario()
        {
            Group errorGroup = new Group() { Name = "New Group 12345678901"};
            Dictionary<string, string> requiredFields = new Dictionary<string, string>();
            requiredFields.Add("Name", "Must be between 1 and 20 characters long.");

            return new ErrorScenarioContext<Group>("MaxLength", errorGroup, requiredFields);
        }

        private ErrorScenarioContext<Group> GetRequiredFieldsErrorScenario()
        {
            Group entity = new Group();
            Dictionary<string, string> requiredFields = new Dictionary<string, string>();
            requiredFields.Add("Name", "Group name cannot be blank.");

            return new ErrorScenarioContext<Group>("RequiredFields", entity, requiredFields);
        }

        protected override Group GetDummyEntity()
        {
            Group dummyGroup = new Group();
            dummyGroup.PopulateDummyValues();
            return dummyGroup;
        }

        protected override Group GetEntityToBeSaved(Group inputEntity)
        {
            inputEntity.Name = "Updated Group Name";

            return inputEntity;
        }
    }
}
