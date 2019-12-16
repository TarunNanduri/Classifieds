using System;
using System.ComponentModel.DataAnnotations;

namespace Classifieds.App.Models
{
    public class Advertisement
    {
        [Required] public int Id { get; set; }
        [Required] public int UserId { get; set; }
        [Required] public int AdTypeId { get; set; }
        [Required] public int CategoryId { get; set; }

        [DataType(DataType.Date)] public DateTime PostedOn { get; set; }

        public int OfferCount { get; set; }
        public int ExpiryDays { get; set; }
        public int ViewCount { get; set; }
        public bool Deleted { get; set; }
        public bool Expired { get; set; }
        public bool Reported { get; set; }
        public int StatusId { get; set; }
    }
}