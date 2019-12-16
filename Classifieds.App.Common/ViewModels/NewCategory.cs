using System.Collections.Generic;

namespace Classifieds.App.Api.ViewModels
{
    public class NewCategory
    {
        public string Name { get; set; }
        public int Icon { get; set; }
        public string Description { get; set; }
        public List<NewAttribute> Attributes { get; set; }
        public int CreatedBy { get; set; }
    }
}