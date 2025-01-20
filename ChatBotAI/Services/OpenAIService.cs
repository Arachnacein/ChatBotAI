using ChatBotAI.Models;

namespace ChatBotAI.Services
{
    public class OpenAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        public OpenAIService(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
        }
        public async Task<string> RetrieveAnswerAsync(string prompt)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var requestBody = new
            {
                model = "gpt-4o",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                max_tokens = 50,
                temperature = 0.7
            };

            using var response = await _httpClient
                .PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);

            if (!response.IsSuccessStatusCode)
                return "Failed to get OpenAI answer.";

            var jsonResponse = await response.Content.ReadFromJsonAsync<OpenAIResponse>();
            var message = jsonResponse?.Choices?.FirstOrDefault()?.Message?.Content;
            return string.IsNullOrWhiteSpace(message) ? "Answer is empty or white spaces" : message;
        }
    }
}