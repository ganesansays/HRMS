using Hrms.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hrms.Models
{
    public class ModelContainer<T> : ICRUDModelContainer
        where T : new()
    {

        public ModelContainer(string name)
        {
            this.Name = name;
            this.DomainValueDictionary = new Dictionary<string, IEnumerable<SelectListItem>>();
        }

        public void SetValues(Mode mode, IReadOnlyList<T> listOfItems, T instance)
        {
            this.Instance = instance == null ? new T() : instance;
            this.ListOfItems = listOfItems;
            this.Mode = mode;
        }

        public void SetValues(Mode mode, IReadOnlyList<T> listOfItems)
        {
            SetValues(mode, listOfItems, default(T));
        }

        public string Name { get; private set; }

        public T Instance { get; set; }

        IReadOnlyList<T> ListOfItems { get; set; }

        public Mode Mode { get; set; }

        public void AddSelectList(string key, IEnumerable<SelectListItem> value)
        {
            DomainValueDictionary.Add(key, value);
        }

        public IEnumerable<SelectListItem> GetSelectList(string key)
        {
            IEnumerable<SelectListItem> Value = null;
            DomainValueDictionary.TryGetValue(key, out Value);
            return Value;
        }

        public Dictionary<string, IEnumerable<SelectListItem>> DomainValueDictionary { get; private set; }

        public dynamic ModelInstance
        {
            get
            {
                return Instance;
            }
        }

        public dynamic ModelListOfItems
        {
            get
            {
                return ListOfItems;
            }
        }
    }
}