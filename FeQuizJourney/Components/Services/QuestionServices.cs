using FeQuizJourney.Components.Models;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Text.Json;

namespace FeQuizJourney.Components.Services
{
    public class QuestionServices
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        public QuestionServices(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }
        public async Task<List<QuestionResponse>?> GetQuestionByIdAsync(int roomId)
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

            if (string.IsNullOrEmpty(token))
                return null;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"http://localhost:5121/api/questions/{roomId}");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to fetch questions: " + response.StatusCode);
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var questions = JsonSerializer.Deserialize<List<QuestionResponse>>(json, options);

            return questions;
        }

    }
}
