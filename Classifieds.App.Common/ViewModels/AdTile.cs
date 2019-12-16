using System;

namespace Classifieds.App.Api.ViewModels
{
    public class AdTile
    {
        public int IconId { get; set; }
        public int AdvertisementId { get; set; }
        public string Category { get; set; }
        public string AdType { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? OfferedBy { get; set; }
    }
}