using LeetSpeakTranslator.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeetSpeakTranslator.Data
{
    public class TranslationContext: IdentityDbContext<IdentityUser>
    {

        public TranslationContext(DbContextOptions<TranslationContext> options)
          : base(options)
        {
        }

        public DbSet<TranslationRecord> TranslationRecords { get; set; }
    }
}
