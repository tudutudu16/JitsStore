using System.ComponentModel.DataAnnotations;

namespace JitsStore.Models
{
    public partial class User
    {
        public Guid Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string UserName { get; set; }

    }
}
