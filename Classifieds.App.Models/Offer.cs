using System;

namespace Classifieds.App.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public int AdvertisementId { get; set; }
        public int Price { get; set; }
        public string Msg { get; set; }
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}