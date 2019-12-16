using System;
using System.Collections.Generic;

namespace Classifieds.App.Api.ViewModels
{
    public class AdDetail
    {
        public int CategoryType { get; set; }
        public int AdTypeId { get; set; }
        public List<Attributes> Attributes { get; set; }
        public int PostedBy { get; set; }
        public int OfferCount { get; set; }
        public DateTime PostedDateTime { get; set; }
        public int ExpiryDays { get; set; }
        public int Views { get; set; }
        public List<string> Images { get; set; }
    }
}