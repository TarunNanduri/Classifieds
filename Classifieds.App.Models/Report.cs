namespace Classifieds.App.Models
{
    public class Report
    {
        public int Id { get; set; }
        public int AdvertisementId { get; set; }
        public string Description { get; set; }
        public bool IsVerified { get; set; }
        public int UserId { get; set; }
    }
}