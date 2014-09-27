using HRMS.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRMS.Models
{
    public class ModelContainer<T> : ICRUDModelContainer
        where T : new()
    {

        public ModelContainer(string Name)
        {
            this.Name = Name;
            this.DomainValueDictionary = new Dictionary<string, IEnumerable<SelectListItem>>();
        }

        public void SetValues(Mode Mode, List<T> ListOfItems, T Instance = default(T))
        {
            this.Instance = Instance == null ? new T() : Instance;
            this.ListOfItems = ListOfItems;
            this.Mode = Mode;
        }

        public string Name { get; private set; }

        public T Instance { get; set; }

        public List<T> ListOfItems { get; set; }

        public Mode Mode { get; set; }

        public IEnumerable<SelectListItem> GetSelectList(string Key)
        {
            IEnumerable<SelectListItem> Value = null;
            DomainValueDictionary.TryGetValue(Key, out Value);
            return Value;
        }

        public Dictionary<string, IEnumerable<SelectListItem>> DomainValueDictionary { get; private set; }
    }
}