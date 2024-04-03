using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LeetSpeakTranslator.Models
{
    public class TranslationRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OriginalText { get; set; }

        [Required]
        public string TranslatedText { get; set; }

        public DateTime TranslationDate { get; set; }

        public string UserId { get; set; } 
        public IdentityUser? User { get; set; }
    }
}
