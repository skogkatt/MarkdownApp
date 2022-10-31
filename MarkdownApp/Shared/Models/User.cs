using System.ComponentModel.DataAnnotations;

#nullable disable


namespace MarkdownApp.Shared.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
