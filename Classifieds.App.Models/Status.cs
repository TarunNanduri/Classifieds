namespace Classifieds.App.Models
{
    public class Status
    {
        public int Id { get; set; }
        public int AdvertisementId { get; set; }
        public string Msg { get; set; }
    }
}