namespace Classifieds.App.Models
{
    public class Attribute
    {
        public int Id { get; set; }
        public int AdvertisementId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}