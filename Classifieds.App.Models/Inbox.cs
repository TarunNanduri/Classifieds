namespace Classifieds.App.Models
{
    public class Inbox
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Offers { get; set; }
        public int Reports { get; set; }
        public int Expired { get; set; }
        public int Deleted { get; set; }
    }
}