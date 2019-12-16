namespace Classifieds.App.Models
{
    public class AttributeDetail
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsMandatory { get; set; }
    }
}