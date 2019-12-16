using System;
using System.Collections.Generic;

namespace Classifieds.App.Api.ViewModels
{
    public class AdPage
    {
        public int UserId { get; set; }
        public int IconId { get; set; }
        public DateTime PostedDateTime { get; set; }
        public List<string> Images { get; set; }
        public List<Attributes> Attributes { get; set; }
        public bool isReported { get; set; }
        public bool isDeleted { get; set; }
        public int ExpiresIn { get; set; }
        public int OfferCount { get; set; }
        public int ViewCount { get; set; }
        public string Category { get; set; }
    }
}