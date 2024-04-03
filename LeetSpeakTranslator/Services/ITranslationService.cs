namespace LeetSpeakTranslator.Services
{
    public interface ITranslationService
    {
        Task<string> TranslateToLeetAsync(string text);
    }
}
