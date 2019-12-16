using System;

namespace Classifieds.App.Models
{
    public class ChatBox
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AdvertisementId { get; set; }
        public string Msg { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}