using FeQuizJourney.Components.Models;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using static FeQuizJourney.Components.Pages.Teacher.CreateRoom;
using static System.Net.WebRequestMethods;

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

        public async Task<StudentAnswerResponse?> SubmitAnswerAsync(int questionId, int selectedChoiceId, int timeTaken)
        {
            var payload = new StudentAnswerRequest
            {
                QuestionId = questionId,
                SelectedChoiceId = selectedChoiceId,
                TimeTaken = timeTaken
            };

            // Ambil token dari localStorage
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5121/api/studentanswer/")
            {
                Content = JsonContent.Create(payload)
            };

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<StudentAnswerResponse>();
                return result;
            }

            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Submit gagal: {error}");
            return null;
        }

        public async Task<List<UserScoreResponse>?> GetUserScoresByRoomIdAsync(int roomId)
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

            if (string.IsNullOrEmpty(token))
                return null;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"http://localhost:5121/api/studentanswer/room/{roomId}/scores");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to fetch scores: " + response.StatusCode);
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var scores = JsonSerializer.Deserialize<List<UserScoreResponse>>(json, options);

            return scores;
        }
                
        public async Task<QuestionResponse> CreateQuestionAsync(CreateQuestionRequest request)
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

            if (string.IsNullOrEmpty(token))
                throw new Exception("Token tidak ditemukan.");

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5121/api/questions/")
            {
                Content = JsonContent.Create(request)
            };
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(httpRequest);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Gagal membuat pertanyaan: {response.StatusCode} - {error}");
            }

            var question = await response.Content.ReadFromJsonAsync<QuestionResponse>();
            return question!;
        }



    }
}
