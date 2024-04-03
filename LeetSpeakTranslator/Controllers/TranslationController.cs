using LeetSpeakTranslator.Data;
using LeetSpeakTranslator.Models;
using LeetSpeakTranslator.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeetSpeakTranslator.Controllers
{
    public class TranslationController : Controller
    {
        private readonly ITranslationService _translationService;
        private readonly TranslationContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TranslationController(ITranslationService translationService, UserManager<IdentityUser> userManager, TranslationContext context)
        {
            _translationService = translationService;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var translations = await _context.TranslationRecords
                .Where(t => t.UserId == userId)
                .ToListAsync();

            return View(translations);
        }


        [HttpPost]
        public async Task<IActionResult> Translate(string text)
        {
            var translatedText = await _translationService.TranslateToLeetAsync(text);
            var userId = _userManager.GetUserId(User);
            var record = new TranslationRecord
            {
                OriginalText = text,
                TranslatedText = translatedText,
                TranslationDate = DateTime.Now,
                UserId = userId
            };
            _context.TranslationRecords.Add(record);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var record = await _context.TranslationRecords.FindAsync(id);
            if (record == null)
            {
                return NotFound();
            }

            _context.TranslationRecords.Remove(record);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index"); 
        }


    }
}
