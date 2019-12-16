namespace Classifieds.App.Models
{
    public class Image
    {
        public int Id { get; set; }
        public int AdvertisementId { get; set; }
        public string Img { get; set; }
    }
}