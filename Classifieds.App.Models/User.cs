using System.ComponentModel.DataAnnotations;

namespace Classifieds.App.Models
{
    public class User
    {
        [Required] public int Id { get; set; }

        [Required] public string Name { get; set; }

        [EmailAddress] public string MailId { get; set; }

        [Phone] public string ContactNo { get; set; }

        [Required] public int LocationId { get; set; }

        public string Photo { get; set; }

        [Required] public string UserRole { get; set; }
    }
}