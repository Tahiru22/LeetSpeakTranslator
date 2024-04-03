using Newtonsoft.Json;

namespace LeetSpeakTranslator.Services
{
    public class TranslationService:ITranslationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<TranslationService> _logger;
        public TranslationService(HttpClient httpClient, ILogger<TranslationService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }


        public async Task<string> TranslateToLeetAsync(string text)
        {
            var response = await _httpClient.GetAsync($"https://api.funtranslations.com/translate/leetspeak.json?text={Uri.EscapeDataString(text)}");
            var contentType = response.Content.Headers.ContentType.MediaType;

            if (contentType == "application/json")
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(content);
                return result.contents.translated;
            }
            else
            {
                
                var content = await response.Content.ReadAsStringAsync();
                
                _logger.LogWarning("Non-JSON response received: {Content}", content);
                return "Error: Non-JSON response";
            }
        }

    }
}
