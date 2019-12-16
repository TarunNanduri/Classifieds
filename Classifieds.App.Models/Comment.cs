using System;

namespace Classifieds.App.Models
{
    public class Comment
    {
        public int AdvertisementId { get; set; }
        public int Id { get; set; }
        public int CommentedBy { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}