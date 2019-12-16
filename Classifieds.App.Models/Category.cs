using System;

namespace Classifieds.App.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Icon { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }
}